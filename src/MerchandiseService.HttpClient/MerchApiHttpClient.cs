using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.HttpClient.Models;
using MerchandiseService.HttpModels;
using Newtonsoft.Json;

namespace MerchandiseService.HttpClient
{
    internal class MerchApiHttpClient : IMerchApiHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public MerchApiHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<Merch> RequestMerch(MerchRequest model, CancellationToken token)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model));

            var response = await _httpClient.PostAsync("v1/api/merch", content, token);
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Merch>(responseBody);
        }
        
        public async Task<Merch> GetMerch(long merchId, CancellationToken token)
        {
            var response = await _httpClient.GetAsync($"v1/api/merch/${merchId}", token);
            var responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Merch>(responseBody);
        }
    }
}