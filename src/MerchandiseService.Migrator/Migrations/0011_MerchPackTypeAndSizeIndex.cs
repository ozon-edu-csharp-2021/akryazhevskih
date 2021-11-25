using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(11)]
    public class MerchPackTypeAndSizeIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_pack_type_size_idx ON merch_packs (type, size);"
            );
        }
    }
}
