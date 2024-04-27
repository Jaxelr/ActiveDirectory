using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveDirectory.Extensions;

public static class CorsExtensions
{
    private const string Policy = "DefaultPolicy";

    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(Policy,
                           builder =>
                           {
                               builder
                               .AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                           });
        });

        return builder;
    }

    public static WebApplication UseCorst(this WebApplication app)
    {
        app.UseCors(Policy);

        return app;
    }
}
