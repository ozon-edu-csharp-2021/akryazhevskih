using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.SupplyShipped;

namespace MerchandiseService.Infrastructure.Handlers.Supply
{
    public class SupplyShippedCommandHandler : IRequestHandler<SupplyShippedCommand, IEnumerable<Merch>>
    {
        private readonly IMerchRepository _merchRepository;

        public SupplyShippedCommandHandler(
            IMerchRepository merchRepository)
        {
            _merchRepository = merchRepository ?? throw new ArgumentNullException(nameof(merchRepository), "Cannot be null");
        }
        
        public async Task<IEnumerable<Merch>> Handle(SupplyShippedCommand command, CancellationToken cancellationToken)
        {
            var merchesInWork = new List<Merch>();
            
            foreach (var supplyItem in command.SupplyItems)
            {
                var availableQuantity = supplyItem.Quantity;
                
                var merches = await _merchRepository.GetSupplyAwaitsMerches(supplyItem.Sku, cancellationToken);

                foreach (var merch in merches.OrderByDescending(x => x.CreatedAt))
                {
                    if (merchesInWork.Contains(merch))
                    {
                        continue;
                    }
                    
                    var merchItem = merch.GetMerchItems().FirstOrDefault(x => x.Sku.Equals(new Sku(supplyItem.Sku)));

                    var neededQuantity = merchItem.Quantity.Value - merchItem.IssuedQuantity.Value;
                    
                    var quantity = availableQuantity >= neededQuantity
                        ? neededQuantity
                        : availableQuantity;

                    merch.SetStatusInWork();
                    merchesInWork.Add(merch);
                    
                    availableQuantity -= quantity;
                    
                    if (availableQuantity == 0)
                    {
                        break;
                    }
                }
            }

            return merchesInWork;
        }
    }
}