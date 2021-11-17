using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.Services
{
    public class StockService : IStockService
    {
        public Task<StockItem> GetStockItem(long sku, CancellationToken token = default)
        {
            var stockItem = new StockItem
            {
                Sku = 1234,
                Quantity = 22
            };

            return Task.FromResult(stockItem);
        }

        public Task<bool> ReserveStockItem(StockItem item, CancellationToken token = default)
        {
            var result = item.Sku > 10000;

            return Task.FromResult(result);
        }
    }
}