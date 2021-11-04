using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion;
using MerchandiseService.Infrastructure.GrpcClients.StockApi;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class CheckMerchPackExpansionCommandHandler : IRequestHandler<CheckMerchPackExpansionCommand, bool>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IStockApiClient _stockApiClient;

        private readonly ILogger _logger;

        public CheckMerchPackExpansionCommandHandler(
            IMerchRepository merchRepository,
            IMerchItemRepository merchItemRepository,
            IMerchPackRepository merchPackRepository,
            IStockApiClient stockApiClient,
            ILogger<CreateMerchCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Сan't be null");
            _merchItemRepository = merchItemRepository ?? throw new ArgumentNullException(nameof(merchItemRepository), "Сan't be null");
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository), "Сan't be null");
            _stockApiClient = stockApiClient ?? throw new ArgumentNullException(nameof(stockApiClient), "Сan't be null");
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Сan't be null");
        }

        public async Task<bool> Handle(CheckMerchPackExpansionCommand command, CancellationToken cancellationToken)
        {
            var merch = await _merchRepository.GetAsync(new Identifier(command.MerchId), cancellationToken);
            if (merch is null)
            {
                throw new MerchNullException($"Merch with ID {command.MerchId} not found");
            }

            var merchPack = await _merchPackRepository.GetAsync(merch.Type, merch.Employee.Size, cancellationToken);

            if (merchPack?.Items == null || merchPack.Items.Any())
            {
                throw new MerchPackNullException(
                    $"Merch pack for type {merch.Type.Name} and size {merch.Employee.Size.Name} not found");
            }

            var result = false;
            
            foreach (var item in merchPack.Items)
            {
                var existing = merch.Items.FirstOrDefault(x => x.Sku.Equals(item.Sku));

                if (existing is null)
                {
                    var available = await _stockApiClient.GetStockItem(item.Sku.Code, cancellationToken);
                    if (available == null)
                    {
                        _logger.LogWarning($"Could not find position by sku {item.Sku.Code}({item.Sku.Name})");
                        continue;
                    }

                    if (available.Quantity == 0)
                    {
                        continue;
                    }

                    var quantity = available.Quantity >= item.Quantity.Value
                        ? item.Quantity.Value
                        : available.Quantity;
                    
                    var isSuccessIssue = await _stockApiClient.IssueStockItem(new StockApiMerchItem
                    {
                        Sku = item.Sku.Code,
                        Quantity = quantity
                    }, cancellationToken);

                    if (isSuccessIssue)
                    {
                        var merchItem = new MerchItem(merch.Id, item.Sku, existing.Quantity, new Quantity(quantity), item.Size);
                        
                        merch.TryUpdateMerchItem(merchItem);

                        await _merchItemRepository.UpdateAsync(merchItem, cancellationToken);
                    }

                    result = true;
                    
                    continue;
                }

                if (!existing.Quantity.Equals(item.Quantity))
                {
                    var available = await _stockApiClient.GetStockItem(item.Sku.Code, cancellationToken);
                    if (available == null)
                    {
                        _logger.LogWarning($"Could not find position by sku {item.Sku.Code}({item.Sku.Name})");
                        continue;
                    }

                    if (available.Quantity == 0)
                    {
                        continue;
                    }

                    var needQuantity = existing.Quantity.Value - item.Quantity.Value;
                    
                    var quantity = available.Quantity >= needQuantity
                        ? needQuantity
                        : available.Quantity;

                    var isSuccessIssue = await _stockApiClient.IssueStockItem(new StockApiMerchItem
                    {
                        Sku = item.Sku.Code,
                        Quantity = quantity
                    }, cancellationToken);

                    if (isSuccessIssue)
                    {
                        var merchItem = new MerchItem(merch.Id, item.Sku, existing.Quantity, new Quantity(quantity), item.Size);
                        
                        merch.TryUpdateMerchItem(merchItem);

                        await _merchItemRepository.UpdateAsync(merchItem, cancellationToken);
                    }
                    
                    result = true;
                }
            }

            return result;
        }
    }
}