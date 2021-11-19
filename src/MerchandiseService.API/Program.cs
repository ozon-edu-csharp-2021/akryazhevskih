using MerchandiseService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MerchandiseService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { 
                    webBuilder.UseUrls("http://*:5000;http://*:5004;")
                              .UseStartup<Startup>(); 
                })
                .AddInfrastructure();
    }
}