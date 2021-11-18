using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class MerchItemsTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS merch_items(
                    id BIGSERIAL PRIMARY KEY,
                    merch_id BIGINT NOT NULL,
                    sku BIGINT NOT NULL,
                    quantity INT NOT NULL,
                    issued_quantity INT,
                    size INT,
                    status INT NOT NULL
                );"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_items;");
        }
    }
}