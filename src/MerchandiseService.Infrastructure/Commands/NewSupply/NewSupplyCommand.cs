using System.Collections.Generic;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Infrastructure.Commands.NewSupply
{
    public class NewSupplyCommand : IRequest<List<Merch>>
    {
        /// <summary>
        /// Идентификатор поставки
        /// </summary>
        public long SupplyId { get; init; }
        
        /// <summary>
        /// Список товаров в поставке
        /// </summary>
        public IEnumerable<SupplyItem> Items { get; set; }
    }
}