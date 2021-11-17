using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.Services
{
    public interface IStockService
    {
        Task<StockItem> GetStockItem(long sku, CancellationToken token = default);

        Task<bool> ReserveStockItem(StockItem item, CancellationToken token = default);
    }
}