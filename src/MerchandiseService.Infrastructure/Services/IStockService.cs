using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.Services
{
    public interface IStockService
    {
        Task<bool> ReserveStockItem(StockItem item, CancellationToken token = default);

        Task<IEnumerable<StockItem>> GetStockItemsAvailability(IEnumerable<long> skus, CancellationToken cancellationToken = default);
    }
}