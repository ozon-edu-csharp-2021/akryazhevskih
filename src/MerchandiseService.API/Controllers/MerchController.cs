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
        [ProducesResponseType(typeof(Merch), StatusCodes.Status200OK)]
        public async Task<ActionResult<Merch>> RequestMerch(
            MerchRequest request,
            CancellationToken token)
        {
            var result = new Merch
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
        [ProducesResponseType(typeof(List<Merch>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Merch>>> GetMerchList(
            [FromQuery] long employeeId,
            CancellationToken token)
        {
            var result = new List<Merch>
            {
                new Merch
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