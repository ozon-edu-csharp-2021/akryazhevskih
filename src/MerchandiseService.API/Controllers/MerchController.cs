using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.API.Models;
using MerchandiseService.HttpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.API.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        /// <summary>
        /// Запрос на выдачу мерча 
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MerchRequest), StatusCodes.Status200OK)]
        public async Task<ActionResult<MerchRequest>> CreateMerchRequest(
            CreateMerchRequestModel request,
            CancellationToken token)
        {
            var result = new MerchRequest
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                EmployeeId = request.EmployeeId
            };

            return Ok(result);
        }
        
        /// <summary>
        /// Получение информации о выданом мерче
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<MerchRequest>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MerchRequest>>> GetMerchRequestList(
            [FromQuery] long employeeId,
            CancellationToken token)
        {
            var result = new List<MerchRequest>
            {
                new MerchRequest
                {
                    Id = Guid.NewGuid(),
                    Type = MerchType.VeteranPack,
                    EmployeeId = employeeId
                }
            };

            return Ok(result);
        }
    }
}