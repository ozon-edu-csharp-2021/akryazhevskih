using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(14)]
    public class EmployeesTableUpdate : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql("DROP TABLE IF EXISTS employees;");
        }
    }
}