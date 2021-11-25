using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Infrastructure.Configuration;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace MerchandiseService.Infrastructure.Repositories.Infrastructure.Implementation
{
    internal class DbConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private readonly DatabaseConnectionOptions _connectionOptions;

        private NpgsqlConnection? _connection;

        public DbConnectionFactory(
            IOptions<DatabaseConnectionOptions> connectionOptions)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), "Cannot be null");
            }

            _connectionOptions = connectionOptions.Value;
        }

        public async Task<NpgsqlConnection> CreateConnection(CancellationToken cancellationToken)
        {
            if (_connection is not null)
            {
                return _connection;
            }

            _connection = new NpgsqlConnection(_connectionOptions.ConnectionString);
            await _connection.OpenAsync();

            _connection.StateChange += ConnectionStateChangeEventHandler;

            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        private void ConnectionStateChangeEventHandler(object sender, StateChangeEventArgs args)
        {
            if (args.CurrentState is ConnectionState.Closed)
            {
                _connection = null;
            }
        }
    }
}
