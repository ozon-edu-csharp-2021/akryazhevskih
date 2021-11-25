using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(6)]
    public class FillMerchPacks : Migration
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
                 SELECT id, 1, 2, 1 FROM rows;"
            );

            // WelcomePack S
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (10, 2, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер S')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 2, 2, 2 FROM rows;"
            );

            // WelcomePack M
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (10, 3, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер M')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 3, 2, 3 FROM rows;"
            );

            // WelcomePack L
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (10, 4, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер L')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 4, 2, 4 FROM rows;"
            );

            // WelcomePack XL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (10, 5, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер XL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 5, 2, 5 FROM rows;"
            );

            // WelcomePack XXL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (10, 6, 'Набор мерча, выдаваемый сотруднику при устройстве на работу. Размер XXL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 6, 2, 6 FROM rows;"
            );


            /// ConferenceListenerPack
            // ConferenceListenerPack XS
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 1, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер XS')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 25, 1, 1 FROM rows;"
            );

            // ConferenceListenerPack S
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 2, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер S')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 26, 1, 2 FROM rows;"
            );

            // ConferenceListenerPack M
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 3, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер M')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 27, 1, 3 FROM rows;"
            );

            // ConferenceListenerPack L
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 4, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер L')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 28, 1, 4 FROM rows;"
            );

            // ConferenceListenerPack XL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 5, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер XL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 29, 1, 5 FROM rows;"
            );

            // ConferenceListenerPack XXL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (20, 6, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя. Размер XXL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 30, 1, 6 FROM rows;"
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
                 SELECT id, 19, 1, 1 FROM rows;"
            );

            // ConferenceSpeakerPack S
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (30, 2, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер S')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 20, 1, 2 FROM rows;"
            );

            // ConferenceSpeakerPack M
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (30, 3, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер M')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 21, 1, 2 FROM rows;"
            );

            // ConferenceSpeakerPack L
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (30, 4, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер L')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 22, 1, 4 FROM rows;"
            );

            // ConferenceSpeakerPack XL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (30, 5, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер XL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 23, 1, 5 FROM rows;"
            );

            // ConferenceSpeakerPack XXL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (30, 6, 'Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера. Размер XXL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 24, 1, 6 FROM rows;"
            );


            /// ProbationPeriodEndingPack
            // ProbationPeriodEndingPack XS
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 1, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер XS')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 7, 2, 1 FROM rows
                UNION
                 SELECT id, 13, 1, 1 FROM rows;"
            );

            // ProbationPeriodEndingPack S
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 2, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер S')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 8, 2, 2 FROM rows
                UNION
                 SELECT id, 14, 1, 2 FROM rows;"
            );

            // ProbationPeriodEndingPack M
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 3, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер M')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 9, 2, 3 FROM rows
                UNION
                 SELECT id, 15, 1, 3 FROM rows;"
            );

            // ProbationPeriodEndingPack L
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 4, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер L')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 10, 2, 4 FROM rows
                UNION
                 SELECT id, 16, 1, 4 FROM rows;"
            );

            // ProbationPeriodEndingPack XL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 5, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер XL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 11, 2, 5 FROM rows
                UNION
                 SELECT id, 17, 1, 5 FROM rows;"
            );

            // ProbationPeriodEndingPack XXL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (40, 6, 'Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока. Размер XXL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 12, 2, 6 FROM rows
                UNION
                 SELECT id, 18, 1, 6 FROM rows;"
            );


            /// VeteranPack 
            // VeteranPack XS
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 1, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер XS')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 31, 3, 1 FROM rows
                UNION
                 SELECT id, 36, 2, 1 FROM rows;"
            );

            // VeteranPack S
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 2, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер S')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 320, 3, 2 FROM rows
                UNION
                 SELECT id, 37, 2, 2 FROM rows;"
            );

            // VeteranPack M
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 3, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер M')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 32, 3, 3 FROM rows
                UNION
                 SELECT id, 380, 2, 3 FROM rows;"
            );

            // VeteranPack L
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 4, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер L')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 33, 3, 4 FROM rows
                UNION
                 SELECT id, 390, 2, 4 FROM rows;"
            );

            // VeteranPack XL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 5, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер XL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 34, 3, 5 FROM rows
                UNION
                 SELECT id, 400, 2, 5 FROM rows;"
            );

            // VeteranPack XXL
            Execute.Sql(@"
                WITH rows AS (
                 INSERT INTO merch_packs (type, size, description)
                 VALUES (50, 6, 'Набор мерча, выдаваемый сотруднику за выслугу лет. Размер XXL')
                 RETURNING id, type, size, description
                )
                INSERT INTO merch_pack_items (merch_pack_id, sku, quantity, size)
                 SELECT id, 35, 3, 6 FROM rows
                UNION
                 SELECT id, 410, 2, 6 FROM rows;"
            );
        }

        public override void Down()
        {
            Execute.Sql("DELETE FROM merch_pack_items;");
            Execute.Sql("DELETE FROM merch_packs;");
        }
    }
}