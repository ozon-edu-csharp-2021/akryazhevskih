namespace MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchItemDb
    {
        public long Id { get; set; }

        public long MerchId { get; set; }

        public long Sku { get; set; }

        public int Quantity { get; set; }

        public int IssuedQuantity { get; set; }

        public int? Size { get; set; }

        public int Status { get; set; }
    }
}
