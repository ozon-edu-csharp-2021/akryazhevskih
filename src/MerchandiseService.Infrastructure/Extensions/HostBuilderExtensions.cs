using System;
using System.IO;
using System.Reflection;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using MerchandiseService.Infrastructure.Filters;
using MerchandiseService.Infrastructure.Interceptors;
using MerchandiseService.Infrastructure.StartupFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Unofficial.CoreFx;
using Serilog;

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
                    var serviceName = Assembly.GetEntryAssembly()?.GetName().Name;

                    options.SwaggerDoc("v1", new OpenApiInfo { Title = serviceName, Version = "v1" });

                    options.CustomSchemaIds(x => x.FullName);

                    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{serviceName}.xml");
                    options.IncludeXmlComments(xmlFilePath);
                });

                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

                // Adds the Jaeger Tracer.
                services.AddSingleton<ITracer>(sp =>
                {
                    var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                    var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender())
                        .Build();

                    var tracer = new Tracer.Builder(serviceName)
                        .WithSampler(new ConstSampler(true))
                        .WithReporter(reporter)
                        .Build();
                    return tracer;
                });

                services.Configure<HttpHandlerDiagnosticOptions>(options =>
                    options.OperationNameResolver =
                        request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");
            });

            builder.UseSerilog((context, configuration) => configuration
                .ReadFrom
                .Configuration(context.Configuration)
                .WriteTo.Console());

            return builder;
        }
    }
}