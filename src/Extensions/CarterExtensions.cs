using ActiveDirectory.Models.Internal;
using Carter;
using Carter.Cache;
using Microsoft.AspNetCore.Builder;

namespace ActiveDirectory.Extensions;

public static class CarterExtensions
{
    public static WebApplicationBuilder AddCarter(this WebApplicationBuilder builder, AppSettings settings)
    {
        if (settings.Cache.CacheEnabled)
        {
            builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
        }

        builder.Services.AddCarter();

        return builder;
    }

    public static WebApplication UseCarter(this WebApplication app, AppSettings settings)
    {
        if (settings.Cache.CacheEnabled)
        {
            app.UseCarterCaching();
        }

        app.MapCarter();

        return app;
    }
}
