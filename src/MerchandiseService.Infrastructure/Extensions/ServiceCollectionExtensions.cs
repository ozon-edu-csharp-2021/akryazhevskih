using MediatR;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Handlers.DomainEvent;
using MerchandiseService.Infrastructure.Handlers.MerchAggregate;
using MerchandiseService.Infrastructure.Handlers.Supply;
using MerchandiseService.Infrastructure.Repositories.MerchPackRepository;
using MerchandiseService.Infrastructure.Repositories.MerchRepository;
using MerchandiseService.Infrastructure.Services;
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
            // Command handlers
            services.AddMediatR(typeof(CreateMerchCommandHandler).Assembly);
            services.AddMediatR(typeof(GetMerchCommandHandler).Assembly);
            services.AddMediatR(typeof(CheckMerchPackExpansionCommandHandler).Assembly);
            services.AddMediatR(typeof(SupplyShippedCommandHandler).Assembly);
            
            // Event handlers
            services.AddMediatR(typeof(MerchStatusChangedToInWorkDomainEventHandler).Assembly);
            services.AddMediatR(typeof(MerchStatusChangedToSupplyAwaitsDomainEventHandler).Assembly);
            services.AddMediatR(typeof(MerchStatusChangedToDoneDomainEventHandler).Assembly);
            
            return services;
        }
        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, DataBaseContext>();
            
            services.AddScoped<IMerchRepository, MerchRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IStockService, StockService>();
            
            return services;
        }
    }
}