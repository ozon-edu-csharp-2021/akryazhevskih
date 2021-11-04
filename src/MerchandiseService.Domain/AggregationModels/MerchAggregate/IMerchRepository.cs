using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    public interface IMerchRepository : IRepository<Merch>
    {
        Task<Merch> GetAsync(Identifier id, CancellationToken cancellationToken = default);

        Task<Merch> GetAsync(Identifier employeeId, MerchType type, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<MerchItem>> GetSupplyAwaitsItems(string sku, CancellationToken cancellationToken = default);
    }
}