using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Infrastructure.Commands.CheckMerchPackExpansion;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.MerchAggregate
{
    public class CheckMerchPackExpansionCommandHandler : IRequestHandler<CheckMerchPackExpansionCommand, bool>
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly ILogger _logger;

        public CheckMerchPackExpansionCommandHandler(
            IMerchRepository merchRepository,
            IMerchPackRepository merchPackRepository,
            ILogger<CheckMerchPackExpansionCommandHandler> logger)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public async Task<bool> Handle(CheckMerchPackExpansionCommand command, CancellationToken cancellationToken)
        {
            var merch = await _merchRepository.GetAsync(command.MerchId, cancellationToken);
            if (merch is null)
            {
                throw new MerchNullException($"Merch with ID {command.MerchId} not found");
            }

            var result = false;

            var merchPack = await _merchPackRepository.GetAsync(merch.Type, merch.Employee.Size, cancellationToken);
            if (merchPack is null)
            {
                throw new MerchNullException($"Merch pack with type {merch.Type.Name} and size {merch.Employee.Size.Name} not found");
            }

            var merchPackItems = merchPack.GetItems();

            foreach (var item in merchPackItems)
            {
                var existing = merch.GetItems().FirstOrDefault(x => x.Sku.Equals(item.Sku));

                if (existing is null)
                {
                    var merchItem = MerchItem.Create(merch.Id, item.Sku, item.Quantity, item.Size);

                    if (!merch.TryAddMerchItem(merchItem, out var reason))
                    {
                        _logger.LogWarning($"Failed to add item: {reason}");
                    }
                    else
                    {
                        result = true;
                    }

                    continue;
                }

                if (existing.Quantity.Value < item.Quantity.Value)
                {
                    existing.SetQuantity(new Quantity(item.Quantity.Value));

                    result = true;
                }
            }

            if (result)
            {
                await _merchRepository.UpdateAsync(merch, cancellationToken);
            }

            return result;
        }
    }
}