using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Товар в наборе
    /// </summary>
    public class MerchItem : Entity
    {
        public MerchItem(
            Identifier merchId,
            Sku sku,
            Quantity quantity,
            Quantity issuedQuantity,
            Size size = null
        )
        {
            if (merchId is null)
            {
                throw new MerchItemException("ID cannot be null");
            }
            
            if (sku is null)
            {
                throw new MerchItemException("Sku cannot be null");
            }
            
            if (quantity is null)
            {
                throw new MerchItemException("Quantity cannot be null");
            }
            
            if (issuedQuantity is null)
            {
                throw new MerchItemException("Issued quantity cannot be null");
            }
            
            MerchId = merchId;
            Sku = sku;
            Quantity = quantity;
            IssuedQuantity = issuedQuantity;
            Size = size;
        }
        
        /// <summary>
        /// Мерч ID
        /// </summary>
        public new Identifier MerchId { get; private set; } 
        
        /// <summary>
        /// Артикул
        /// </summary>
        public Sku Sku { get; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public Quantity Quantity { get; private set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public Quantity IssuedQuantity { get; private set; }
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; }

        /// <summary>
        /// Статус
        /// </summary>
        public MerchItemStatus Status =>
            Quantity.Equals(IssuedQuantity) ? MerchItemStatus.Done : MerchItemStatus.SupplyAwaits;

        public void SetQuantity(Quantity quantity)
        {
            if (quantity is null)
            {
                throw new QuantityException("Quantity cannot be null");
            }
            
            if (quantity.Value <= 0)
            {
                throw new QuantityException("Quantity cannot be less than or equal to 0");
            }
            
            Quantity = quantity;
        }
        public void SetIssuedQuantity(Quantity quantity)
        {
            if (quantity is null)
            {
                throw new QuantityException("Issued quantity cannot be null");
            }
            
            if (quantity.Value < 0)
            {
                throw new QuantityException("Issued quantity cannot be less than 0");
            }
            
            if (quantity.Value > Quantity.Value)
            {
                throw new QuantityException("Issued quantity cannot be more than necessary quantity");
            }
            
            IssuedQuantity = quantity;
        }
    }
}