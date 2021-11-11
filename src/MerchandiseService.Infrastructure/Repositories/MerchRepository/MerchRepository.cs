using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;

namespace MerchandiseService.Infrastructure.Repositories.MerchRepository
{
    public class MerchRepository : IMerchRepository
    {
        private IUnitOfWork _context;

        public MerchRepository(IUnitOfWork context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Cannot be null");
        }
        
        public IUnitOfWork UnitOfWork => _context;
        
        public Task<Merch> CreateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(merch);
        }

        public Task<Merch> UpdateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(merch);
        }

        public Task<Merch> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            if (id < 1000)
            {
                return Task.FromResult<Merch>(null);
            }

            var merch = new Merch(
                new Employee(999, Size.XL, new Email("test999@test.ru")),
                MerchType.VeteranPack
            );

            merch.TryAddMerchItem(new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL),
                out var reason
            );
            
            return Task.FromResult(merch);
        }

        public Task<Merch> GetAsync(long employeeId, MerchType type, CancellationToken cancellationToken = default)
        {
            if (employeeId < 1000)
            {
                return Task.FromResult<Merch>(null);
            }
            
            var merch = new Merch(
                new Employee(employeeId, Size.XL, new Email("test999@test.ru")),
                type
            );

            merch.TryAddMerchItem(new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL),
                out var reason
            );
            
            return Task.FromResult(merch);
        }

        public Task<IEnumerable<Merch>> GetSupplyAwaitsMerches(long sku, CancellationToken cancellationToken = default)
        {
            var merch = new Merch(
                new Employee(999, Size.XL, new Email("test999@test.ru")),
                MerchType.VeteranPack
            );

            merch.TryAddMerchItem(new MerchItem(
                    new Sku(123456),
                    new Quantity(10),
                    Size.XL),
                out var reason
            );

            IEnumerable<Merch> merches = new List<Merch>
            {
                merch
            };
            
            return Task.FromResult(merches);
        }
    }
}