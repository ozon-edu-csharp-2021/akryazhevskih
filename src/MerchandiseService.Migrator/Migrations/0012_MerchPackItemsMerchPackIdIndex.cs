using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(12)]
    public class MerchPackItemsMerchPackIdIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_pack_items_merch_pack_id_idx ON merch_pack_items (merch_pack_id);"
            );
        }
    }
}
