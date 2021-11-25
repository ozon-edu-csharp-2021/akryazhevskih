using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(8)]
    public class MerchEmployeeAndTypeIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX merch_employee_id_type_idx ON merches (employee_id, type);"
            );
        }
    }
}
