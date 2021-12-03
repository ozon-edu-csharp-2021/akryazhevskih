using System.Threading;
using CSharpCourse.Core.Lib.Events;

namespace MerchandiseService.Infrastructure.MessageBroker
{
    public interface IMerchProducer
    {
        void Publish(long merchId, NotificationEvent notificationEvent, CancellationToken cancellationToken = default);
    }
}
