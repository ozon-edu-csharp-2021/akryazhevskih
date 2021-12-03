namespace MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchPackItemDb
    {
        public long Id { get; set; }

        public long MerchPackId { get; set; }

        public long Sku { get; set; }

        public int Quantity { get; set; }

        public int? Size { get; set; }
    }
}
