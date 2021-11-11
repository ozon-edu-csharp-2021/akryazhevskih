using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Domain.Events.Merch
{
    public class AddMerchItemDomainEvent : INotification
    {
        public AddMerchItemDomainEvent(long merchId, MerchItem merchItem)
        {
            MerchId = merchId;
            MerchItem = merchItem;
        }
        
        public long MerchId { get; }
        
        public MerchItem MerchItem { get; }
    }
}