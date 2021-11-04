using MerchandiseService.Domain.Models;

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
        /// Ждет поставки
        /// </summary>
        public static MerchStatus SupplyAwaits = new(2, nameof(SupplyAwaits));
        
        /// <summary>
        /// Выдан
        /// </summary>
        public static MerchStatus Done = new(3, nameof(Done));
    }
}