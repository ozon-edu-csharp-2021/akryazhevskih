using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(10)]
    public class MerchItemsSkuAndStatusIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_items_sku_status_idx ON merch_items (sku, status);"
            );
        }
    }
}
