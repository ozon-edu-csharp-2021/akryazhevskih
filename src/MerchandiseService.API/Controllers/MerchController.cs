using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.API.Models;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.HttpModels;
using MerchandiseService.Infrastructure.Commands.CreateMerch;
using MerchandiseService.Infrastructure.Commands.GetMerch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.API.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MerchController(
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Cannot be null");
        }

        /// <summary>
        /// Запрос на выдачу мерча
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Merch), StatusCodes.Status200OK)]
        public async Task<ActionResult<Merch>> RequestMerch(
            MerchRequest request,
            CancellationToken token)
        {
            var createMerchCommand = new CreateMerchCommand
            {
                MerchType = request.Type,
                EmployeeId = request.EmployeeId.Value,
                Size = request.Size,
                Email = request.Email
            };

            try
            {
                var merch = await _mediator.Send(createMerchCommand, token);

                var result = new Merch
                {
                    Id = merch.Id,
                    Type = (MerchType)merch.Type.Id,
                    Status = (MerchStatus)merch.Status.Id,
                    EmployeeId = merch.Employee.Id,
                    CreateAt = merch.CreatedAt,
                    IssuedAt = merch.IssuedAt,
                    Items = merch.GetItems().Select(x => new MerchItem
                    {
                        Sku = x.Sku.Code,
                        Quantity = x.Quantity.Value,
                        IssuedQuantity = x.IssuedQuantity == null ? 0 : x.IssuedQuantity.Value,
                        Size = x.Size is null ? null : (Size)x.Size.Id,
                        Status = (MerchItemStatus)x.Status.Id
                    })
                };

                return Ok(result);
            }
            catch (MerchAlreadyExistException)
            {
                return Conflict();
            }
            catch (MerchPackNullException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Получение информации о мерче
        /// </summary>
        [HttpGet("{merchId}")]
        [ProducesResponseType(typeof(Merch), StatusCodes.Status200OK)]
        public async Task<ActionResult<Merch>> GetMerch(
            [Range(1, long.MaxValue)] long merchId,
            CancellationToken token)
        {
            var getMerchCommand = new GetMerchCommand
            {
                MerchId = merchId
            };

            var merch = await _mediator.Send(getMerchCommand, token);

            if (merch is null)
            {
                return NotFound();
            }

            var result = new Merch
            {
                Id = merch.Id,
                Type = (MerchType)merch.Type.Id,
                Status = (MerchStatus)merch.Status.Id,
                EmployeeId = merch.Employee.Id,
                CreateAt = merch.CreatedAt,
                IssuedAt = merch.IssuedAt,
                Items = merch.GetItems().Select(x => new MerchItem
                {
                    Sku = x.Sku.Code,
                    Quantity = x.Quantity.Value,
                    IssuedQuantity = x.IssuedQuantity == null ? 0 : x.IssuedQuantity.Value,
                    Size = x.Size is null ? null : (Size)x.Size.Id,
                    Status = (MerchItemStatus)x.Status.Id
                })
            };

            return Ok(result);
        }
    }
}