using MerchandiseService.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MerchandiseService.Infrastructure.Filters
{
    internal class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var model = new GlobalExceptionModel
            {
                Name = context.Exception.GetType().FullName,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new JsonResult(model)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}