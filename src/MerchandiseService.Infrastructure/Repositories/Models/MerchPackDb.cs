namespace MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchPackDb
    {
        public long Id { get; set; }

        public int Type { get; set; }

        public int? Size { get; set; }

        public string Description { get; set; }
    }
}
