using MediatR;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Configuration;
using MerchandiseService.Infrastructure.Repositories.Implementation.EmployeeAggregate;
using MerchandiseService.Infrastructure.Repositories.Implementation.MerchRepository;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Implementation;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using MerchandiseService.Infrastructure.Repositories.MerchPackRepository;
using MerchandiseService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;

namespace MerchandiseService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)));

            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, DbConnectionFactory>();
            services.AddScoped<IUnitOfWork, DbContext>();
            services.AddScoped<IChangeTracker, ChangeTracker>();

            services.AddScoped<IMerchRepository, MerchRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IStockService, StockService>();
            
            return services;
        }
    }
}