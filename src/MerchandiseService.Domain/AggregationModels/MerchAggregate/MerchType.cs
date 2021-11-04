using System.Linq;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Тип набора
    /// </summary>
    public class MerchType : Enumeration
    {
        public MerchType(int id, string name)
            : base(id, name)
        {
        }
        
        public static MerchType WelcomePack = new(10, nameof(WelcomePack));
        public static MerchType ConferenceListenerPack = new(20, nameof(ConferenceListenerPack));
        public static MerchType ConferenceSpeakerPack = new(30, nameof(ConferenceSpeakerPack));
        public static MerchType ProbationPeriodEndingPack = new(40, nameof(ProbationPeriodEndingPack));
        public static MerchType VeteranPack = new(50, nameof(VeteranPack));

        public static bool TryParse(int value, out MerchType merchType)
        {
            if (value < 1)
            {
                throw new MerchTypeException("ID cannot be less than 1");
            }
            
            merchType = GetAll<MerchType>().FirstOrDefault(x => x.Id.Equals(value));

            return merchType is not null;
        }
    }
}