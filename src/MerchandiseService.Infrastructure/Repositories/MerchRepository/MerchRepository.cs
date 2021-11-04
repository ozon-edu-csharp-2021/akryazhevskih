using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace MerchandiseService.Infrastructure.Repositories.MerchRepository
{
    public class MerchRepository : IMerchRepository
    {
        public Task<Merch> CreateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(merch);
        }

        public Task<Merch> UpdateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(merch);
        }

        public Task<Merch> GetAsync(Identifier id, CancellationToken cancellationToken = default)
        {
            if (id.Value < 1000)
            {
                return Task.FromResult<Merch>(null);
            }

            var merch = new Merch(
                new Employee(new Identifier(999), Size.XL, new Email("test999@test.ru")),
                MerchType.VeteranPack
            );

            merch.TryAddMerchItem(new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL)
            );
            
            return Task.FromResult(merch);
        }

        public Task<Merch> GetAsync(Identifier employeeId, MerchType type, CancellationToken cancellationToken = default)
        {
            if (employeeId.Value < 1000)
            {
                return Task.FromResult<Merch>(null);
            }
            
            var merch = new Merch(
                new Employee(employeeId, Size.XL, new Email("test999@test.ru")),
                type
            );

            merch.TryAddMerchItem(new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL)
            );
            
            return Task.FromResult(merch);
        }

        public Task<IEnumerable<MerchItem>> GetSupplyAwaitsItems(string sku, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}