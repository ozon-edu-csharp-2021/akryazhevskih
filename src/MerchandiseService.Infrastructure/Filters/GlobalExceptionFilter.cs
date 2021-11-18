using MerchandiseService.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace MerchandiseService.Infrastructure.Filters
{
    internal class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        public override void OnException(ExceptionContext context)
        {
            var model = new GlobalExceptionModel
            {
                Name = context.Exception.GetType().FullName,
                StackTrace = context.Exception.StackTrace
            };

            _logger.LogError(context.Exception, $"TraceId={context.HttpContext.TraceIdentifier} Service error.");

            context.Result = new JsonResult(model)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}