using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class MerchPacksTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS merch_packs(
                    id BIGSERIAL PRIMARY KEY,
                    type INT NOT NULL,
                    size INT,
                    description TEXT
                );"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_packs;");
        }
    }
}