using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.NewSupply;
using MerchandiseService.Infrastructure.GrpcClients.StockApi;
using MerchandiseService.Infrastructure.Handlers.MerchAggregate;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.SupplyAggregate
{
    public class NewSupplyCommandHandler : IRequestHandler<NewSupplyCommand, List<Merch>>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IStockApiClient _stockApiClient;

        private readonly ILogger _logger;
        
        public NewSupplyCommandHandler(
            IMerchRepository merchRepository,
            IMerchItemRepository merchItemRepository,
            IStockApiClient stockApiClient,
            ILogger<CreateMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Сan't be null");
            _merchItemRepository = merchItemRepository ?? throw new ArgumentNullException(nameof(merchItemRepository), "Сan't be null");
            _stockApiClient = stockApiClient ?? throw new ArgumentNullException(nameof(stockApiClient), "Сan't be null");
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Сan't be null");
        }

        public async Task<List<Merch>> Handle(NewSupplyCommand command, CancellationToken cancellationToken)
        {
            var notificationList = new List<Merch>();
            
            foreach (var supplyItem in command.Items)
            {
                var items = await _merchRepository.GetSupplyAwaitsItems(supplyItem.Sku, cancellationToken);

                var availableQuantity = supplyItem.Quantity;
                foreach (var merchItem in items)
                {
                    var needQuantity = merchItem.Quantity.Value - merchItem.IssuedQuantity.Value;

                    var quantity = availableQuantity >= needQuantity ? needQuantity : availableQuantity;
                    
                    var isSuccessIssue = await _stockApiClient.IssueStockItem(new StockApiMerchItem
                    {
                        Sku = merchItem.Sku.Code,
                        Quantity = quantity
                    }, cancellationToken);
                        
                    if (isSuccessIssue)
                    {
                        merchItem.SetIssuedQuantity(new Quantity(quantity));
                        await _merchItemRepository.UpdateAsync(merchItem, cancellationToken);
                        
                        var merch = await _merchRepository.GetAsync(merchItem.MerchId, cancellationToken);
                        
                        notificationList.Add(merch);
                    }
                }
            }

            return notificationList;
        }
    }
}