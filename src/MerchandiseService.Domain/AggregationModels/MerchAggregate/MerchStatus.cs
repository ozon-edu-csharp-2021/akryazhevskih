using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Models;
using System.Linq;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Статус мерча
    /// </summary>
    public class MerchStatus : Enumeration
    {
        private MerchStatus(int id, string name)
            : base(id, name)
        {
        }
        
        /// <summary>
        /// Новый
        /// </summary>
        public static MerchStatus New = new(1, nameof(New));
        
        /// <summary>
        /// В работе
        /// </summary>
        public static MerchStatus InWork = new(2, nameof(InWork));
        
        /// <summary>
        /// Ждет поставки
        /// </summary>
        public static MerchStatus SupplyAwaits = new(3, nameof(SupplyAwaits));
        
        /// <summary>
        /// Выдан
        /// </summary>
        public static MerchStatus Done = new(4, nameof(Done));

        public static MerchStatus Parse(int value)
        {
            if (value < 1)
            {
                throw new MerchTypeException("ID cannot be less than 1");
            }

            return GetAll<MerchStatus>().FirstOrDefault(x => x.Id.Equals(value));
        }
    }
}