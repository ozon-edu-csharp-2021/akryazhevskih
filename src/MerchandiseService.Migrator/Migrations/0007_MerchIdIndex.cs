using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(7)]
    public class MerchIdIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_id_idx ON merches (id);"
            );
        }
    }
}
