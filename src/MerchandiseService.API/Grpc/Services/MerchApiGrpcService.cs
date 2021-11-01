using System;
using System.Threading.Tasks;
using Grpc.Core;
using MerchandiseService.Grpc;

namespace MerchandiseService.API.Grpc.Services
{
    public class MerchApiGrpcService : MerchApiGrpc.MerchApiGrpcBase
    {
        /// <summary>
        /// Запрос на выдачу мерча 
        /// </summary>
        public override Task<MerchResponse> RequestMerch(
            MerchRequest request,
            ServerCallContext context)
        {
            var result = new MerchResponse
            {
                Merch = new Merch
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = request.Type,
                    EmployeeId = request.EmployeeId
                }
            };

            return Task.FromResult(result);
        }

        /// <summary>
        /// Получение информации о выданом мерче
        /// </summary>
        public override Task<GetMerchListResponse> GetMerchList(
            GetMerchListRequest request,
            ServerCallContext context)
        {
            var result = new GetMerchListResponse
            {
                Items =
                {
                    new Merch
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = MerchType.VeteranPack,
                        EmployeeId = request.EmployeeId
                    }
                }
            };

            return Task.FromResult(result);
        }
    }
}