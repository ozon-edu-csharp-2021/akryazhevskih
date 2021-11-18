using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToSupplyAwaitsDomainEvent : INotification
    {
        public MerchStatusChangedToSupplyAwaitsDomainEvent(
            Merch merch)
        {
            Merch = merch;
        }

        public Merch Merch { get; }
    }
}