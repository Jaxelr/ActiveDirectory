using System.Threading.Tasks;
using ActiveDirectory.Models.Internal;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ActiveDirectory.Modules;

public class MainModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("/", (AppSettings settings, HttpContext ctx) =>
        {
            ctx.Response.Redirect(settings.RouteDefinition.RouteSuffix);
            return Task.CompletedTask;
        });
}
