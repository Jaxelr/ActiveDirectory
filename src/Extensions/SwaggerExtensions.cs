using ActiveDirectory.Models.Internal;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ActiveDirectory.Extensions;

public static class SwaggerExtensions
{
    private const string ServiceName = "Active Directory";

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder, AppSettings settings)
    {
        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(settings.RouteDefinition.Version, new OpenApiInfo
            {
                Description = ServiceName,
                Version = settings.RouteDefinition.Version,
            });

            options.DocInclusionPredicate((_, description) =>
            {
                foreach (object metaData in description.ActionDescriptor.EndpointMetadata)
                {
                    if (metaData is IIncludeOpenApi)
                    {
                        return true;
                    }
                }
                return false;
            });
        });

        return builder;
    }

    public static WebApplication UseSwagger(this WebApplication app, AppSettings _)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
