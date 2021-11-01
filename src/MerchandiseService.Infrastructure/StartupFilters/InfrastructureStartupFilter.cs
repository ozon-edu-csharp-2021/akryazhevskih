using System;
using MerchandiseService.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace MerchandiseService.Infrastructure.StartupFilters
{
    internal class InfrastructureStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseWhen(context => context.Request.ContentType != "application/grpc", builder =>
                {
                    builder.UseMiddleware<LoggingMiddleware>();
                });
                
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/ready", builder => builder.UseMiddleware<ReadyMiddleware>());
                app.Map("/live", builder => builder.UseMiddleware<LiveMiddleware>());
                
                
                next(app);
            };
        }
    }
}