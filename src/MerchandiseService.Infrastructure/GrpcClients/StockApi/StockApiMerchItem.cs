namespace MerchandiseService.Infrastructure.GrpcClients.StockApi
{
    public class StockApiMerchItem
    {
        public long Sku { get; set; }
        
        public int Quantity { get; set; }
    }
}