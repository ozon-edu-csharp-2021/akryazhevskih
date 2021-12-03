using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class MerchesTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS merches(
                    id BIGSERIAL PRIMARY KEY,
                    employee_id BIGINT NOT NULL,
                    status INT NOT NULL,
                    type INT NOT NULL,
                    created_at TIMESTAMP NOT NULL,
                    issued_at TIMESTAMP
                );"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merches;");
        }
    }
}