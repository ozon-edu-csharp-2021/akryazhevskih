using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(15)]
    public class MerchesTableUpdate : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                ALTER TABLE merches DROP COLUMN employee_id;
                ALTER TABLE merches ADD COLUMN size INT NOT NULL;
                ALTER TABLE merches ADD COLUMN employee_name TEXT NOT NULL;
                ALTER TABLE merches ADD COLUMN employee_email TEXT NOT NULL;
                ALTER TABLE merches ADD COLUMN manager_name TEXT NOT NULL;
                ALTER TABLE merches ADD COLUMN manager_email TEXT NOT NULL;"
            );
        }
    }
}