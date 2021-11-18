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
            long id,
            long merchId,
            Sku sku,
            Quantity quantity,
            Quantity issuedQuantity,
            Size size = null)
        {
            SetId(id);
            MerchId = merchId;
            Sku = sku;
            Quantity = quantity;
            IssuedQuantity = issuedQuantity;
            Size = size;
        }

        public static MerchItem Create(long merchId, Sku sku, Quantity quantity, Size size = null)
        {
            return new MerchItem(merchId, sku, quantity, size);
        }

        private MerchItem(
            long merchId,
            Sku sku,
            Quantity quantity,
            Size size = null)
        {
            if (merchId <= 0)
            {
                throw new MerchItemException("Merch ID cannot be less than 1");
            }

            if (sku is null)
            {
                throw new MerchItemException("Sku cannot be null");
            }
            
            if (quantity is null)
            {
                throw new MerchItemException("Quantity cannot be null");
            }

            MerchId = merchId;
            Sku = sku;
            Quantity = quantity;
            Size = size;
        }

        /// <summary>
        /// ID мерча
        /// </summary>
        public long MerchId { get; private set; }
        
        /// <summary>
        /// Товарная позиция
        /// </summary>
        public Sku Sku { get; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public Quantity Quantity { get; protected set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public Quantity IssuedQuantity { get; protected set; }
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; }

        /// <summary>
        /// Статус
        /// </summary>
        public MerchItemStatus Status => Quantity.Equals(IssuedQuantity) ? MerchItemStatus.Done : MerchItemStatus.Awaits;

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
            
            if (quantity.Value <= 0)
            {
                throw new QuantityException("Issued quantity cannot be less than or equal to 0");
            }
            
            if (quantity.Value > Quantity.Value)
            {
                throw new QuantityException("Issued quantity cannot be more than necessary quantity");
            }
            
            IssuedQuantity = quantity;
        }

        protected void SetId(long id)
        {
            Id = id;
        }
    }
}