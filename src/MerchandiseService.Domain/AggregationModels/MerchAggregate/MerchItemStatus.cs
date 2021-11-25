using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Статус товара в наборе
    /// </summary>
    public class MerchItemStatus : Enumeration
    {
        /// <summary>
        /// Ждет поставки
        /// </summary>
        public static MerchItemStatus Awaits = new (1, nameof(Awaits));

        /// <summary>
        /// Выдан
        /// </summary>
        public static MerchItemStatus Done = new (2, nameof(Done));

        private MerchItemStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}