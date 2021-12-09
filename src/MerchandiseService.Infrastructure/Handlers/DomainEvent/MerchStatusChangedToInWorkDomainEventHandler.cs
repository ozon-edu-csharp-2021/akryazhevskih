using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Domain.Events;
using MerchandiseService.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class MerchStatusChangedToInWorkDomainEventHandler : INotificationHandler<MerchStatusChangedToInWorkDomainEvent>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IStockService _stockService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MerchStatusChangedToInWorkDomainEventHandler(
            IMerchRepository merchRepository,
            IStockService stockService,
            IUnitOfWork unitOfWork,
            ILogger<MerchStatusChangedToInWorkDomainEventHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService), "Cannot be null");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public async Task Handle(
            MerchStatusChangedToInWorkDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var merch = notification.Merch;
            var merchId = merch.Id;

            _logger.LogInformation($"Merch with ID: {merchId} went to status {MerchStatus.InWork}");

            var isDone = false;
            var merchItems = merch.GetItems().Where(x => x.Status.Equals(MerchItemStatus.Awaits));

            var skus = merchItems.Select(x => x.Sku.Code);

            var stockItems = await _stockService.GetStockItemsAvailability(skus, cancellationToken);

            foreach (var item in merchItems)
            {
                var available = stockItems.FirstOrDefault(x => x.Sku == item.Sku.Code);
                if (available == null)
                {
                    _logger.LogWarning($"Could not find position by sku {item.Sku.Code}");

                    continue;
                }

                if (available.Quantity == 0)
                {
                    continue;
                }

                var neededQuantity = item.IssuedQuantity == null ? item.Quantity.Value : item.Quantity.Value - item.IssuedQuantity.Value;
                var quantity = available.Quantity >= neededQuantity
                                ? neededQuantity
                                : available.Quantity;

                var result = await _stockService.ReserveStockItem(
                    new StockItem
                    {
                        Sku = item.Sku.Code,
                        Quantity = quantity
                    },
                    cancellationToken);

                if (result)
                {
                    item.SetIssuedQuantity(new Quantity(quantity));

                    await _merchRepository.UpdateAsync(item, cancellationToken);
                    isDone = true;
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
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}