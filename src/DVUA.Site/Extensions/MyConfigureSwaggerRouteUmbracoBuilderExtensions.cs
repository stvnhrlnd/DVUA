using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

using DVUA.Site.Filters;

namespace DVUA.Site.Extensions;

public static class MyConfigureSwaggerRouteUmbracoBuilderExtensions
{
    // call this from Program.cs, i.e.:
    //     CreateUmbracoBuilder()
    //         ...
    //         .ConfigureMySwaggerRoute()
    //         .Build();
    public static IUmbracoBuilder ConfigureMySwaggerRoute(this IUmbracoBuilder builder)
    {
        builder.Services.Configure<UmbracoPipelineOptions>(options =>
        {
            // include this line if you do NOT want the Swagger docs at /umbraco/swagger
            options.PipelineFilters.RemoveAll(filter => filter is SwaggerRouteTemplatePipelineFilter);

            // setup your own Swagger routes
            options.AddFilter(new MySwaggerRouteTemplatePipelineFilter("MyApi"));
        });
        return builder;
    }
}
