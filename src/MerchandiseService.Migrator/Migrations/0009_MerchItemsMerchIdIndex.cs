using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(9)]
    public class MerchItemsMerchIdIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_items_merch_id_idx ON merch_items (merch_id);"
            );
        }
    }
}
