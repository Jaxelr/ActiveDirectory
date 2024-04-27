using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ActiveDirectory.Extensions;

public static class HealthcheckExtensions
{
    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();

        return builder;
    }

    public static WebApplication UseHealthChecks(this WebApplication app)
    {
        app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
        {
            ResponseWriter = WriteResponse
        });

        return app;
    }

    private static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var json = new
        {
            statusCode = report.Status,
            status = report.Status.ToString(),
            timelapsed = report.TotalDuration
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(json));
    }
}
