using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(13)]
    public class EmployeeIdIndex : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX employee_id_idx ON employees (id);"
            );
        }
    }
}
