using System.Collections.Generic;
using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Entities;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ActiveDirectory.Modules;

public class GroupModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("/GroupUser/{group}", async (string group, AppSettings settings, IAdRepository repository, HttpContext ctx) =>
            await ctx.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetGroupUsers(group))
        )
        .Produces<IEnumerable<User>>(StatusCodes.Status200OK)
        .Produces<FailedResponse>(StatusCodes.Status500InternalServerError)
        .WithName("GroupUser")
        .WithTags("Group")
        .IncludeInOpenApi();
}
