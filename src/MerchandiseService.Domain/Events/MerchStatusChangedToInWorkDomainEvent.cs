using MediatR;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToInWorkDomainEvent : INotification
    {
        public MerchStatusChangedToInWorkDomainEvent(long merchId)
        {
            MerchId = merchId;
        }
        
        public long MerchId { get; }
    }
}