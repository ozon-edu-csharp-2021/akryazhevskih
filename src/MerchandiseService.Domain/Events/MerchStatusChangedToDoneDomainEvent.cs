using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToDoneDomainEvent : INotification
    {
        public MerchStatusChangedToDoneDomainEvent(
            Merch merch)
        {
            Merch = merch;
        }

        public Merch Merch { get; }
    }
}