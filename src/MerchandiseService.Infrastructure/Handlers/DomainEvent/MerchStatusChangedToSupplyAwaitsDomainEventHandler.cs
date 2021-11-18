using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.Events;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.Infrastructure.Handlers.DomainEvent
{
    public class MerchStatusChangedToSupplyAwaitsDomainEventHandler : INotificationHandler<MerchStatusChangedToSupplyAwaitsDomainEvent>
    {
        private readonly ILogger _logger;

        public MerchStatusChangedToSupplyAwaitsDomainEventHandler(
            ILogger<MerchStatusChangedToSupplyAwaitsDomainEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }
        
        public Task Handle(
            MerchStatusChangedToSupplyAwaitsDomainEvent notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Merch with ID: {notification.Merch.Id} went to status {MerchStatus.SupplyAwaits}");
            
            return Task.CompletedTask;
        }
    }
}