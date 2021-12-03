using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.Events;
using MerchandiseService.Infrastructure.MessageBroker;
using Microsoft.Extensions.Logging;
using MerchType = CSharpCourse.Core.Lib.Enums.MerchType;

namespace MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class MerchStatusChangedToDoneDomainEventHandler : INotificationHandler<MerchStatusChangedToDoneDomainEvent>
    {
        private readonly IMerchProducer _merchProducer;
        private readonly ILogger _logger;

        public MerchStatusChangedToDoneDomainEventHandler(
            IMerchProducer merchProducer,
            ILogger<MerchStatusChangedToDoneDomainEventHandler> logger)
        {
            _merchProducer = merchProducer ?? throw new ArgumentNullException(nameof(merchProducer), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public Task Handle(
            MerchStatusChangedToDoneDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var merch = notification.Merch;
            var employee = notification.Merch.Employee;
            var manager = notification.Merch.Manager;

            _logger.LogInformation($"Merch with ID: {merch.Id} went to status {MerchStatus.Done}");

            var notificationEvent = new NotificationEvent
            {
                EmployeeEmail = employee.Email.Value,
                EmployeeName = employee.Person.FullName,

                ManagerEmail = manager.Email.Value,
                ManagerName = manager.Person.FullName,

                EventType = EmployeeEventType.MerchDelivery,

                Payload = new MerchDeliveryEventPayload
                {
                    MerchType = (MerchType)merch.Type.Id,
                    ClothingSize = (ClothingSize)merch.Size.Id
                }
            };

            _merchProducer.Publish(merch.Id, notificationEvent, cancellationToken);

            return Task.CompletedTask;
        }
    }
}