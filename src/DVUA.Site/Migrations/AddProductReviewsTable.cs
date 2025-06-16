using Umbraco.Cms.Infrastructure.Migrations;

using DVUA.Site.Models.Schemas;

namespace DVUA.Site.Migrations;

public class AddProductReviewsTable(IMigrationContext context) : MigrationBase(context)
{
    protected override void Migrate()
    {
        Logger.LogDebug("Running migration {MigrationStep}", "AddProductReviewsTable");

        if (TableExists("ProductReviews"))
        {
            Logger.LogDebug("The database table {DbTable} already exists, skipping...", "ProductReviews");
            return;
        }

        Create.Table<ProductReviewSchema>().Do();
    }
}
