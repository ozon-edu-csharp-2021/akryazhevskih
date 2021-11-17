using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPackItem : Entity
    {
        /// <summary>
        /// Товар
        /// </summary>
        public MerchPackItem(
            Sku sku,
            Quantity quantity,
            Size size = null)
        {
            if (sku is null)
            {
                throw new MerchPackItemException("Sku cannot be null");
            }
            
            if (quantity is null)
            {
                throw new MerchPackItemException("Quantity cannot be null");
            }
            
            Sku = sku;
            Quantity = quantity;
            Size = size;
        }
        
        /// <summary>
        /// Товарная позиция
        /// </summary>
        public Sku Sku { get; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public Quantity Quantity { get; }
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; }
    }
}