using CSharpCourse.Core.Lib.Events;

namespace MerchandiseService.Infrastructure.HostedServices.Models
{
    internal class IssueMerchNotificationEvent : NotificationEvent
    {
        public new MerchDeliveryEventPayload Payload { get; set; }
    }
}
