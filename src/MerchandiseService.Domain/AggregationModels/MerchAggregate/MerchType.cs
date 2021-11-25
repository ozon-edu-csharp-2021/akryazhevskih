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
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при устройстве на работу.
        /// </summary>
        public static MerchType WelcomePack = new (10, nameof(WelcomePack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя.
        /// </summary>
        public static MerchType ConferenceListenerPack = new (20, nameof(ConferenceListenerPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера.
        /// </summary>
        public static MerchType ConferenceSpeakerPack = new (30, nameof(ConferenceSpeakerPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока.
        /// </summary>
        public static MerchType ProbationPeriodEndingPack = new (40, nameof(ProbationPeriodEndingPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику за выслугу лет.
        /// </summary>
        public static MerchType VeteranPack = new (50, nameof(VeteranPack));

        private MerchType(int id, string name)
            : base(id, name)
        {
        }

        public static bool TryParse(int value, out MerchType? merchType)
        {
            if (value < 1)
            {
                throw new MerchTypeException("ID cannot be less than 1");
            }

            merchType = GetAll<MerchType>().FirstOrDefault(x => x.Id.Equals(value));

            return merchType is not null;
        }

        public static MerchType Parse(int value)
        {
            if (value < 1)
            {
                throw new MerchTypeException("ID cannot be less than 1");
            }

            return GetAll<MerchType>().FirstOrDefault(x => x.Id.Equals(value));
        }
    }
}