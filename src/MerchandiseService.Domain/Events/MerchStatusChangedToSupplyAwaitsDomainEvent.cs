using MediatR;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToSupplyAwaitsDomainEvent : INotification
    {
        public MerchStatusChangedToSupplyAwaitsDomainEvent(long merchId)
        {
            MerchId = merchId;
        }
        
        public long MerchId { get; }
    }
}