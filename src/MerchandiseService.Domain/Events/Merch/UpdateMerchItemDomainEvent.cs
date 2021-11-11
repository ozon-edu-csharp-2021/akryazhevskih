using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;

namespace MerchandiseService.Domain.Events.Merch
{
    public class UpdateMerchItemDomainEvent : INotification
    {
        public UpdateMerchItemDomainEvent(long merchId, MerchItem merchItem)
        {
            MerchId = merchId;
            MerchItem = merchItem;
        }
        
        public long MerchId { get; }
        
        public MerchItem MerchItem { get; }
    }
}