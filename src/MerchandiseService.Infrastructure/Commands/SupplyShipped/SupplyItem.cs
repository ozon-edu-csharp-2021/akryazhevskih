namespace MerchandiseService.Infrastructure.Commands.SupplyShipped
{
    public class SupplyItem
    {
        /// <summary>
        /// Товарная позиция
        /// </summary>
        public long Sku { get; set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }
    }
}