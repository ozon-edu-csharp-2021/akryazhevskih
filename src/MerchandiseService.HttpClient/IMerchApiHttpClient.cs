using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;
using MerchandiseService.HttpModels;

namespace MerchandiseService.HttpClient
{
    public interface IMerchApiHttpClient
    {
        Task<Merch> RequestMerch(MerchRequest model, CancellationToken token);

        Task<Merch> GetMerch(long merchId, CancellationToken token);
    }
}