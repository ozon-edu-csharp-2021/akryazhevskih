using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Exceptions;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Npgsql;

namespace MerchandiseService.Infrastructure.Repositories.Infrastructure.Implementation
{
    internal class DbContext : IUnitOfWork
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _connectionFactory;
        private readonly IPublisher _publisher;
        private readonly IChangeTracker _changeTracker;

        private NpgsqlTransaction _transaction;

        public DbContext(
            IDbConnectionFactory<NpgsqlConnection> connectionFactory,
            IPublisher publisher,
            IChangeTracker changeTracker)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "Cannot be null");
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher), "Cannot be null");
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker), "Cannot be null");
        }

        public async Task StartTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
            {
                return;
            }

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            _transaction = await connection.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
            {
                throw new TransactionNullException("Transaction cannot be null");
            }

            var domainEvents = _changeTracker.TrackedEntities.SelectMany(x =>
            {
                if (x.DomainEvents is null || !x.DomainEvents.Any())
                {
                    return new List<INotification>();
                }

                var events = x.DomainEvents.ToList();
                x.ClearDomainEvents();

                return events;
            });

            var domainEventsQueue = new Queue<INotification>(domainEvents);

            while (domainEventsQueue.TryDequeue(out var domainEvent))
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            await _transaction.CommitAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connectionFactory?.Dispose();
        }
    }
}