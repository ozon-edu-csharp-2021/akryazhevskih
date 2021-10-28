using System.Reflection;
using System.Threading.Tasks;
using MerchandiseService.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MerchandiseService.Infrastructure.Middlewares
{
    internal class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var assembly = Assembly.GetEntryAssembly().GetName();

            var response = new VersionResponse
            {
                Version = assembly.Version?.ToString() ?? "-",
                ServiceName = assembly.Name
            };
            
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}