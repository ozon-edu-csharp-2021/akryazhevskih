using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.GrpcClients.StockApi
{
    public class StockApiClient : IStockApiClient
    {
        public Task<StockApiMerchItem> GetStockItem(long sku, CancellationToken token = default)
        {
            if (sku == 1234567)
            {
                return null;
            }

            var item = new StockApiMerchItem
            {
                Sku = sku,
                Quantity = (int)sku
            };
            
            return Task.FromResult(item);
        }

        public Task<bool> IssueStockItem(StockApiMerchItem item, CancellationToken token = default)
        {
            return Task.FromResult(item.Quantity < 10);
        }
    }
}