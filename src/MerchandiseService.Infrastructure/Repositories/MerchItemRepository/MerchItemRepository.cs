using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace MerchandiseService.Infrastructure.Repositories.MerchItemRepository
{
    internal class MerchItemRepository : IMerchItemRepository
    {
        public Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(itemToCreate);
        }

        public Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(itemToUpdate);
        }

        public Task<List<MerchItem>> GetItemsAsync(MerchType type, Size size, CancellationToken cancellationToken = default)
        {
            var result = new List<MerchItem>
            {
                new MerchItem(
                    new Identifier(999),
                    new Sku(123456, "Socks"),
                    new Quantity(10),
                    new Quantity(10),
                    size
                )
            };

            return Task.FromResult(result);
        }
    }
}