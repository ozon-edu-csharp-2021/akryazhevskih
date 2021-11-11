using MediatR;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToDoneDomainEvent : INotification
    {
        public MerchStatusChangedToDoneDomainEvent(long merchId)
        {
            MerchId = merchId;
        }
        
        public long MerchId { get; }
    }
}