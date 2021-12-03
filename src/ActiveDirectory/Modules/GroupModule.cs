using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ActiveDirectory.Modules;

public class GroupModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("/GroupUser/{group}", (string group, AppSettings settings, IAdRepository repository, HttpResponse res) =>
            res.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetGroupUsers(group)));
}
