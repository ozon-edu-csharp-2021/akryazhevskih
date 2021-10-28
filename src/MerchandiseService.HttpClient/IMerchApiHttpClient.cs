using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;
using MerchandiseService.HttpModels;

namespace MerchandiseService.HttpClient
{
    public interface IMerchApiHttpClient
    {
        Task<MerchRequest> CreateMerchRequest(CreateMerchRequestModel model, CancellationToken token);

        Task<List<MerchRequest>> GetMerchRequestList(long employeeId, CancellationToken token);
    }
}