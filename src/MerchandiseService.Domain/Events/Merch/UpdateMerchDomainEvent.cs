using MediatR;

namespace MerchandiseService.Domain.Events.Merch
{
    public class UpdateMerchDomainEvent : INotification
    {
        public UpdateMerchDomainEvent(AggregationModels.MerchAggregate.Merch merch)
        {
            Merch = merch;
        }
        
        public AggregationModels.MerchAggregate.Merch Merch { get; }
    }
}