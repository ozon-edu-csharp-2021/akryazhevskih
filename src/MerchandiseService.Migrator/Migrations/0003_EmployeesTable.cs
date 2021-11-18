using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class EmployeesTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS employees(
                    id BIGINT PRIMARY KEY,
                    size INT NOT NULL,
                    email TEXT NOT NULL
                );"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS employees;");
        }
    }
}