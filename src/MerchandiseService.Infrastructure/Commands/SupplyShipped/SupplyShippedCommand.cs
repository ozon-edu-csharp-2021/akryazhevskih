using System.Collections.Generic;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Infrastructure.Commands.SupplyShipped
{
    public class SupplyShippedCommand : IRequest<IEnumerable<Merch>>
    {
        public SupplyShippedCommand(long supplyId, IEnumerable<SupplyItem> supplyItems)
        {
            SupplyId = supplyId;
            SupplyItems = supplyItems;
        }
        
        /// <summary>
        /// Идентификаторв поставки
        /// </summary>
        public long SupplyId { get; }
        
        /// <summary>
        /// Товары
        /// </summary>
        public IEnumerable<SupplyItem> SupplyItems { get; }
    }
}