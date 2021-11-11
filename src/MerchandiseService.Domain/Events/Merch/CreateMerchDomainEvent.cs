using MediatR;

namespace MerchandiseService.Domain.Events.Merch
{
    public class CreateMerchDomainEvent : INotification
    {
        public CreateMerchDomainEvent(AggregationModels.MerchAggregate.Merch merch)
        {
            Merch = merch;
        }
        
        public AggregationModels.MerchAggregate.Merch Merch { get; }
    }
}