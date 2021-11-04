namespace MerchandiseService.HttpModels
{
    /// <summary>
    /// Товар
    /// </summary>
    public class MerchItem
    {
        /// <summary>
        /// Артикул
        /// </summary>
        public long Sku { get; set; }
        
        /// <summary>
        /// Наименование 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Количество 
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Размер 
        /// </summary>
        public Size? Size { get; set; }
    }
}