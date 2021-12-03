using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MerchandiseService.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OzonEdu.StockApi.Grpc;

namespace MerchandiseService.Infrastructure.Services
{
    public class StockApiGrpcService : IStockService
    {
        private readonly ILogger _logger;
        private readonly StockApiOptions _stockApiOptions;

        public StockApiGrpcService(
            ILogger<StockApiGrpcService> logger,
            IOptions<StockApiOptions> stockApiOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
            _stockApiOptions = stockApiOptions == null ? throw new ArgumentNullException(nameof(stockApiOptions), "Cannot be null") : stockApiOptions.Value;
        }

        public async Task<bool> ReserveStockItem(StockItem item, CancellationToken cancellationToken = default)
        {
            using var channel = GrpcChannel.ForAddress(_stockApiOptions.Path);
            var client = new StockApiGrpc.StockApiGrpcClient(channel);

            var request = new GiveOutItemsRequest();

            request.Items.Add(new SkuQuantityItem
            {
                Sku = item.Sku,
                Quantity = item.Quantity
            });

            try
            {
                var result = await client.GiveOutItemsAsync(request, cancellationToken: cancellationToken);

                return result.Result is GiveOutItemsResponse.Types.Result.Successful ? true : false;
            }
            catch (RpcException e)
            {
                var message = "Error when getting availability stock items";

                _logger.LogError(e, message);

                throw new InvalidOperationException(message, e);
            }
        }

        public async Task<IEnumerable<StockItem>> GetStockItemsAvailability(IEnumerable<long> skus, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress(_stockApiOptions.Path);
            var client = new StockApiGrpc.StockApiGrpcClient(channel);

            var request = new SkusRequest();
            request.Skus.AddRange(skus);

            try
            {
                var result = await client.GetStockItemsAvailabilityAsync(request, cancellationToken: cancellationToken);

                return result.Items.Select(x => new StockItem
                {
                    Sku = x.Sku,
                    Quantity = x.Quantity
                });
            }
            catch (RpcException e)
            {
                var message = "Error when getting availability stock items";

                _logger.LogError(e, message);

                throw new InvalidOperationException(message, e);
            }
        }
    }
}
