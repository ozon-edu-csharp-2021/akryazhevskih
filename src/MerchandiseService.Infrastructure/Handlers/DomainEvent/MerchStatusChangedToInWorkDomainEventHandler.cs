using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Events;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class MerchStatusChangedToInWorkDomainEventHandler : INotificationHandler<MerchStatusChangedToInWorkDomainEvent>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IStockService _stockService;
        private readonly ILogger _logger;

        public MerchStatusChangedToInWorkDomainEventHandler(
            IMerchRepository merchRepository,
            IStockService stockService,
            ILogger<MerchStatusChangedToInWorkDomainEventHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }
        
        public async Task Handle(
            MerchStatusChangedToInWorkDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var merchId = notification.MerchId;
            
            _logger.LogInformation($"Merch with ID: {merchId} went to status {MerchStatus.InWork}");
            
            var merch = await _merchRepository.GetAsync(merchId, cancellationToken);

            if (merch is null)
            {
                throw new MerchNullException($"Merch with ID: {merchId} not found");
            }

            var isDone = true;
            var merchItems = merch.GetMerchItems().Where(x => x.Status.Equals(MerchItemStatus.Awaits));
            
            foreach (var item in merchItems)
            {
                var available = await _stockService.GetStockItem(item.Sku.Code, cancellationToken);
                if (available == null)
                {
                    _logger.LogWarning($"Could not find position by sku {item.Sku.Code}");
                    
                    isDone = false;
                    continue;
                }

                if (available.Quantity == 0)
                {
                    isDone = false;
                    continue;
                }
                
                var neededQuantity = item.Quantity.Value - item.IssuedQuantity.Value;
                var quantity = available.Quantity >= neededQuantity
                                ? neededQuantity
                                : available.Quantity;

                var result = await _stockService.ReserveStockItem(new StockItem
                {
                    Sku = item.Sku.Code,
                    Quantity = quantity
                }, cancellationToken);

                if (result)
                {
                    item.SetIssuedQuantity(new Quantity(quantity));
                }
            }

            if (isDone)
            {
                merch.SetStatusDone();
            }
            else
            {
                merch.SetStatusSupplyAwaits();
            }

            await _merchRepository.UpdateAsync(merch, cancellationToken);
        }
    }
}