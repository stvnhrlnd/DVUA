using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

using DVUA.Site.Migrations;

namespace DVUA.Site.Composers;

public class DVUAComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunProductReviewsMigration>();
    }
}
