using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPackItem : Entity
    {
        public MerchPackItem(
            long id,
            long merchPackId,
            Sku sku,
            Quantity quantity,
            Size? size = null)
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

        private MerchPackItem(
            long merchPackId,
            Sku sku,
            Quantity quantity,
            Size? size = null)
        {
            if (merchPackId <= 0)
            {
                throw new MerchPackItemException("Merch pack ID cannot be less than 1");
            }

            if (sku is null)
            {
                throw new MerchPackItemException("Sku cannot be null");
            }

            if (quantity is null)
            {
                throw new MerchPackItemException("Quantity cannot be null");
            }

            MerchPackId = merchPackId;
            Sku = sku;
            Quantity = quantity;
            Size = size;
        }

        /// <summary>
        /// ID набора
        /// </summary>
        public long MerchPackId { get; set; }

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

        public static MerchPackItem Create(long merchPackId, Sku sku, Quantity quantity, Size? size = null)
        {
            return new MerchPackItem(merchPackId, sku, quantity, size);
        }
    }
}