using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using MerchandiseService.Infrastructure.Repositories.Models;
using Npgsql;

namespace MerchandiseService.Infrastructure.Repositories.MerchPackRepository
{
    public class MerchPackRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _connectionFactory;
        private readonly IChangeTracker _changeTracker;

        public MerchPackRepository(
            IDbConnectionFactory<NpgsqlConnection> connectionFactory,
            IChangeTracker changeTracker)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "Cannot be null");
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker), "Cannot be null");
        }

        public Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MerchPack> GetAsync(MerchType type, Size size, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, type, size, description
                FROM merch_packs mp
                WHERE type = @type
                AND (size = @size OR size IS NULL);";

            var parameters = new
            {
                type = type.Id,
                size = size?.Id,
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QuerySingleOrDefaultAsync<MerchPackDb>(query, parameters);

            if (result is null)
            {
                return null;
            }

            var merchPack = new MerchPack(
                result.Id,
                MerchType.Parse(result.Type),
                result.Description,
                result.Size.HasValue ? Size.Parse(result.Size.Value) : null);

            var merchPackItems = await GetMerchPackItems(merchPack.Id, cancellationToken);

            merchPack.SetItems(merchPackItems);

            _changeTracker.Track(merchPack);

            return merchPack;
        }

        public async Task<IEnumerable<MerchPackItem>> GetMerchPackItems(long merchPackId, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, merch_pack_id as MerchPackId, sku, quantity, size
                FROM merch_pack_items
                WHERE merch_pack_id = @merch_pack_id;";

            var parameters = new
            {
                merch_pack_id = merchPackId,
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryAsync<MerchPackItemDb>(query, parameters);

            var merchPackItems = result.Select(x => new MerchPackItem(
                x.Id,
                x.MerchPackId,
                new Sku(x.Sku),
                new Quantity(x.Quantity),
                x.Size.HasValue ? Size.Parse(x.Size.Value) : null));

            foreach (var merchPackItem in merchPackItems)
            {
                _changeTracker.Track(merchPackItem);
            }

            return merchPackItems;
        }
    }
}