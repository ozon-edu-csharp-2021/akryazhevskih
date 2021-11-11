using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.Events;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class MerchStatusChangedToDoneDomainEventHandler : INotificationHandler<MerchStatusChangedToDoneDomainEvent>
    {
        private readonly ILogger _logger;

        public MerchStatusChangedToDoneDomainEventHandler(
            ILogger<MerchStatusChangedToDoneDomainEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }
        
        public Task Handle(
            MerchStatusChangedToDoneDomainEvent notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Merch with ID: {notification.MerchId} went to status {MerchStatus.Done}");
            
            // Отправка сообщения на почту
            
            return Task.CompletedTask;
        }
    }
}