using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Infrastructure.GrpcClients.StockApi;
using MerchandiseService.Infrastructure.Handlers.MerchAggregate;
using MerchandiseService.Infrastructure.Repositories.MerchItemRepository;
using MerchandiseService.Infrastructure.Repositories.MerchPackRepository;
using MerchandiseService.Infrastructure.Repositories.MerchRepository;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMediatR(typeof(CheckMerchPackExpansionCommandHandler).Assembly);
            services.AddMediatR(typeof(CreateMerchCommandHandler).Assembly);
            services.AddMediatR(typeof(GetMerchCommandHandler).Assembly);
            
            return services;
        }
        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMerchRepository, MerchRepository>();
            services.AddScoped<IMerchItemRepository, MerchItemRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IStockApiClient, StockApiClient>();
            
            return services;
        }
    }
}