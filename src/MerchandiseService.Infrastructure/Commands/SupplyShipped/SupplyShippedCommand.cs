using System.Collections.Generic;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Infrastructure.Commands.SupplyShipped
{
    public class SupplyShippedCommand : IRequest<IEnumerable<Merch>>
    {
        public SupplyShippedCommand(IEnumerable<SupplyItem> supplyItems)
        {
            SupplyItems = supplyItems;
        }

        /// <summary>
        /// Товары
        /// </summary>
        public IEnumerable<SupplyItem> SupplyItems { get; }
    }
}