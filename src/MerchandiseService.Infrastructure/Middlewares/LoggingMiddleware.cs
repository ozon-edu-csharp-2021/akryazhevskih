using System;
using System.Linq;
using System.Threading.Tasks;
using MerchandiseService.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MerchandiseService.Infrastructure.Middlewares
{
    internal class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogRequest(context);

            await _next(context);

            LogResponse(context);
        }

        private void LogRequest(HttpContext context)
        {
            try
            {
                var request = context.Request;

                var model = new LoggingModel
                {
                    Route = request.Path + request.QueryString,
                    Headers = request.Headers.Select(x => $"{x.Key} = {x.Value}").ToArray()
                };

                _logger.LogInformation($"Service request: {JsonConvert.SerializeObject(model)}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request");
            }
        }

        private void LogResponse(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;

                var model = new LoggingModel
                {
                    Route = request.Path + request.QueryString,
                    Headers = response.Headers.Select(x => $"{x.Key} = {x.Value}").ToArray()
                };

                _logger.LogInformation($"Service response: {JsonConvert.SerializeObject(model)}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response");
            }
        }
    }
}