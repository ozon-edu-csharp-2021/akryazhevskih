using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;

namespace MerchandiseService.HttpClient
{
    public interface IMerchApiHttpClient
    {
        Task<MerchRequest> RequestMerch(MerchRequest model, CancellationToken token);

        Task<List<MerchRequest>> GetMerchList(long employeeId, CancellationToken token);
    }
}