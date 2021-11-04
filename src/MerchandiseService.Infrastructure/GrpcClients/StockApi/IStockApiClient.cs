using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.GrpcClients.StockApi
{
    public interface IStockApiClient
    {
        Task<StockApiMerchItem> GetStockItem(long sku, CancellationToken token = default);

        Task<bool> IssueStockItem(StockApiMerchItem item, CancellationToken token = default);
    }
}