using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Infrastructure.Commands.GetMerch
{
    public class GetMerchCommand : IRequest<Merch>
    {
        /// <summary>
        /// Идентификатор мерча
        /// </summary>
        public long MerchId { get; init; }
    }
}