using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Grpc;
using MerchandiseService.Infrastructure.Commands.CreateMerch;
using MerchandiseService.Infrastructure.Commands.GetMerch;

namespace MerchandiseService.API.Grpc.Services
{
    public class MerchApiGrpcService : MerchApiGrpc.MerchApiGrpcBase
    {
        private readonly IMediator _mediator;

        public MerchApiGrpcService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Cannot be null");
        }

        /// <summary>
        /// Запрос на выдачу мерча
        /// </summary>
        public override async Task<Merch> RequestMerch(
            MerchRequest request,
            ServerCallContext context)
        {
            var createMerchCommand = new CreateMerchCommand
            {
                MerchType = (HttpModels.MerchType)request.Type,
                Size = (HttpModels.Size)request.Size,
                EmployeeName = request.EmployeeName,
                EmployeeEmail = request.EmployeeEmail,
                ManagerName = request.ManagerName,
                ManagerEmail = request.ManagerEmail
            };

            try
            {
                var result = await _mediator.Send(createMerchCommand, context.CancellationToken);

                var merch = new Merch
                {
                    Id = result.Id,
                    Type = (MerchType)result.Type.Id,
                    Status = (MerchStatus)result.Status.Id,
                    Size = (Size)result.Size.Id,
                    EmployeeName = result.Employee.Person.FullName,
                    EmployeeEmail = result.Employee.Email.Value,
                    ManagerName = result.Manager.Person.FullName,
                    ManagerEmail = result.Manager.Email.Value,
                    CreateAt = Timestamp.FromDateTime(result.CreatedAt.ToUniversalTime()),
                    IssuedAt = result.IssuedAt.HasValue ? Timestamp.FromDateTime(result.IssuedAt.Value.ToUniversalTime()) : null
                };

                merch.Items.AddRange(result.GetItems().Select(x => new MerchItem
                {
                    Sku = x.Sku.Code,
                    Quantity = x.Quantity.Value,
                    IssuedQuantity = x.IssuedQuantity == null ? 0 : x.IssuedQuantity.Value,
                    Size = x.Size is null ? Size.Unspecified : (Size)x.Size.Id,
                    Status = (MerchItemStatus)x.Status.Id
                }));

                return merch;
            }
            catch (MerchAlreadyExistException)
            {
                throw new RpcException(new Status(StatusCode.AlreadyExists, $"Merch with type {request.Type} for employee {request.EmployeeEmail} has already been issued"));
            }
            catch (MerchPackNullException)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Merch pack with type {request.Type} not found"));
            }
        }

        /// <summary>
        /// Получение информации о выданом мерче
        /// </summary>
        public override async Task<Merch> GetMerch(
            GetMerchRequest request,
            ServerCallContext context)
        {
            var getMerchCommand = new GetMerchCommand
            {
                MerchId = request.MerchId
            };

            var result = await _mediator.Send(getMerchCommand, context.CancellationToken);

            if (result is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Merch with ID {request.MerchId} not found"));
            }

            var merch = new Merch
            {
                Id = result.Id,
                Type = (MerchType)result.Type.Id,
                Status = (MerchStatus)result.Status.Id,
                Size = (Size)result.Size.Id,
                EmployeeName = result.Employee.Person.FullName,
                EmployeeEmail = result.Employee.Email.Value,
                ManagerName = result.Manager.Person.FullName,
                ManagerEmail = result.Manager.Email.Value,
                CreateAt = Timestamp.FromDateTime(result.CreatedAt.ToUniversalTime()),
                IssuedAt = result.IssuedAt.HasValue ? Timestamp.FromDateTime(result.IssuedAt.Value.ToUniversalTime()) : null
            };

            merch.Items.AddRange(result.GetItems().Select(x => new MerchItem
            {
                Sku = x.Sku.Code,
                Quantity = x.Quantity.Value,
                IssuedQuantity = x.IssuedQuantity == null ? 0 : x.IssuedQuantity.Value,
                Size = x.Size is null ? Size.Unspecified : (Size)x.Size.Id,
                Status = (MerchItemStatus)x.Status.Id
            }));

            return merch;
        }
    }
}