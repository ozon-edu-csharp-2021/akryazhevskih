using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using System.Collections.Generic;

namespace MerchandiseService.Domain.Events
{
    public class MerchStatusChangedToInWorkDomainEvent : INotification
    {
        public MerchStatusChangedToInWorkDomainEvent(
            Merch merch)
        {
            Merch = merch;
        }

        public Merch Merch { get; }
    }
}