using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(6)]
    public class ConfigureMerchPacks : Migration
    {
        public override void Up()
        {
            /// WelcomePack
            // WelcomePack XS
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 1, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер XS')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 1000000201, 1, 1 FROM rows;"
            );

            // WelcomePack S
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 2, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер S')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 2000000201, 1, 2 FROM rows;"
            );

            // WelcomePack M
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 3, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер M')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 3000000201, 1, 3 FROM rows;"
            );

            // WelcomePack L
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 4, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер L')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 4000000201, 1, 4 FROM rows;"
            );

            // WelcomePack XL
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 5, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер XL')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 5000000201, 1, 5 FROM rows;"
            );

            // WelcomePack XXL
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (10, 6, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер XXL')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000101, 1, null FROM rows
                UNION 
	                SELECT id, 6000000201, 1, 6 FROM rows;"
            );


            /// ConferenceListenerPack
            // ConferenceListenerPack
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (20, null, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя.')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows;"
            );


            /// ConferenceSpeakerPack
            // ConferenceSpeakerPack XS
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 1, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер XS')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 1000000501, 1, 1 FROM rows;"
            );

            // ConferenceSpeakerPack S
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 2, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер S')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 2000000501, 1, 2 FROM rows;"
            );

            // ConferenceSpeakerPack M
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 3, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер M')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 3000000501, 1, 3 FROM rows;"
            );

            // ConferenceSpeakerPack L
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 4, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер L')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 4000000501, 1, 4 FROM rows;"
            );

            // ConferenceSpeakerPack XL
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 5, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер XL')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 5000000501, 1, 5 FROM rows;"
            );

            // ConferenceSpeakerPack XXL
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (30, 6, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер XXL')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000401, 1, null FROM rows
                UNION
	                SELECT id, 6000000501, 1, 6 FROM rows;"
            );


            /// ProbationPeriodEndingPack
            // ProbationPeriodEndingPack
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (40, null, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока.')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows;"
            );


            /// VeteranPack 
            // VeteranPack 
            Execute.Sql(@"
                WITH rows AS (
	                INSERT INTO merch_packs (type, size, description)
	                VALUES (50, null, 'Набор мерча, выдаваемый сотруднику за выслугу лет.')
	                RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
	                SELECT id, 1000000301, 2, null::integer FROM rows
                UNION 
	                SELECT id, 1000000601, 10, null FROM rows
                UNION 
	                SELECT id, 1000000101, 1, null FROM rows;"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS merch_pack_items;");
        }
    }
}