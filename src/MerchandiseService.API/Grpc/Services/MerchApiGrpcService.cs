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
        public override Task<MerchRequestModel> CreateMerchRequest(
            CreateMerchRequestModel request,
            ServerCallContext context)
        {
            var result = new MerchRequestModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = request.Type,
                EmployeeId = request.EmployeeId
            };

            return Task.FromResult(result);
        }

        /// <summary>
        /// Получение информации о выданом мерче
        /// </summary>
        public override Task<MerchRequestListModel> GetMerchRequestList(
            GetMerchRequestListModel request,
            ServerCallContext context)
        {
            var result = new MerchRequestListModel
            {
                Items =
                {
                    new MerchRequestModel
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