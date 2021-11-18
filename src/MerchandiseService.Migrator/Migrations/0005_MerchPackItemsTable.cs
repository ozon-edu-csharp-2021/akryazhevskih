using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(5)]
    public class MerchPackItemsTable : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE IF NOT EXISTS merch_pack_items(
                    id BIGSERIAL PRIMARY KEY,
                    merch_pack_id BIGINT NOT NULL,
                    sku BIGINT NOT NULL,
                    quantity INT NOT NULL,
                    size INT
                );"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_pack_items;");
        }
    }
}