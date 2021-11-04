using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace MerchandiseService.Infrastructure.Repositories.MerchPackRepository
{
    public class MerchPackRepository : IMerchPackRepository
    {
        public Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> GetAsync(MerchType type, Size size, CancellationToken cancellationToken = default)
        {
            var itemsXL = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(6),
                    Size.XL),
                new MerchPackItem(
                    new Sku(654321, "T-Shirt"),
                    new Quantity(1),
                    Size.XL)
            };
            
            var itemsL = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(6),
                    Size.L),
                new MerchPackItem(
                    new Sku(654321, "T-Shirt"),
                    new Quantity(1),
                    Size.L)
            };

            var packList = new List<MerchPack>
            {
                new MerchPack(MerchType.WelcomePack, itemsXL, Size.XL),
                new MerchPack(MerchType.WelcomePack, itemsL, Size.L),
                
                new MerchPack(MerchType.ConferenceListenerPack, itemsXL, Size.XL),
                
                new MerchPack(MerchType.ConferenceSpeakerPack, itemsXL, Size.XL),
                
                new MerchPack(MerchType.ProbationPeriodEndingPack, itemsXL, Size.XL),
                
                new MerchPack(MerchType.VeteranPack, itemsXL, Size.XL),
                
                new MerchPack(MerchType.VeteranPack, itemsXL, Size.XL),
                new MerchPack(MerchType.VeteranPack, itemsL, Size.L),
            };

            var pack = packList.FirstOrDefault(x => x.Type.Equals(type) && x.Size.Equals(size));

            return Task.FromResult(pack);
        }
    }
}