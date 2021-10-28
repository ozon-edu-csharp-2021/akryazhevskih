using System;
using System.IO;
using System.Reflection;
using MerchandiseService.Infrastructure.Filters;
using MerchandiseService.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MerchandiseService.Infrastructure.StartupFilters;
using Microsoft.OpenApi.Models;

namespace MerchandiseService.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, InfrastructureStartupFilter>();
                
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSwaggerGen(options =>
                {
                    var serviceName = Assembly.GetEntryAssembly().GetName().Name;
                    
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = serviceName, Version = "v1"});
                
                    options.CustomSchemaIds(x => x.FullName);
                
                    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{serviceName}.xml");
                    options.IncludeXmlComments(xmlFilePath);
                });
                
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
                
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
                
            });
            
            return builder;
        }
    }
}