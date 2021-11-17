﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    public interface IMerchRepository : IRepository<Merch>
    {
        Task<Merch> GetAsync(long id, CancellationToken cancellationToken = default);

        Task<Merch> GetAsync(long employeeId, MerchType type, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<Merch>> GetSupplyAwaitsMerches(long sku, CancellationToken cancellationToken = default);
    }
}