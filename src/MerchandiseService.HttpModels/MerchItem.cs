namespace MerchandiseService.HttpModels
{
    /// <summary>
    /// Товар
    /// </summary>
    public class MerchItem
    {
        /// <summary>
        /// Товарная позиция
        /// </summary>
        public long Sku { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Выданное количество
        /// </summary>
        public int? IssuedQuantity { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public MerchItemStatus Status { get; set; }
    }
}