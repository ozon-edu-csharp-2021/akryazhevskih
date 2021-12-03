using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace MerchandiseService.Infrastructure.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigurePorts(this IWebHostBuilder builder)
        {
            var httpPortEnv = Environment.GetEnvironmentVariable("HTTP_PORT");
            if (!int.TryParse(httpPortEnv, out var httpPort))
            {
                httpPort = 5000;
            }

            var grpcPortEnv = Environment.GetEnvironmentVariable("GRPC_PORT");
            if (!int.TryParse(grpcPortEnv, out var grpcPort))
            {
                grpcPort = 5004;
            }

            builder.ConfigureKestrel(options =>
            {
                // HTTP
                options.Listen(IPAddress.Any, httpPort, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });

                // GRPC
                options.Listen(IPAddress.Any, grpcPort, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            return builder;
        }
    }
}
