using System.Collections.Generic;
using System.Linq;
using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Entities;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Models.Operations;
using ActiveDirectory.Repositories;
using Carter;
using Carter.ModelBinding;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveDirectory.Modules;

public class UserModule : ICarterModule
{
    private const string ModuleTag = "User";
    private const string ApplicationJson = "application/json";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var repository = app.ServiceProvider.GetRequiredService<IAdRepository>();
        var settings = app.ServiceProvider.GetRequiredService<AppSettings>();

        app.MapGet("/User/{username}", async (string username, HttpContext ctx) =>
            await ctx.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetUserInfo(username))
        )
        .Produces<User>(200)
        .Produces(204)
        .Produces<FailedResponse>(500)
        .WithName("User")
        .WithTags(ModuleTag)
        .IncludeInOpenApi();

        app.MapGet("/UserGroup/{username}", async (string username, HttpContext ctx) =>
            await ctx.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetUserGroups(username))
        )
        .Produces<IEnumerable<UserGroup>>(200)
        .Produces(204)
        .Produces<FailedResponse>(500)
        .WithName("UserGroup")
        .WithTags(ModuleTag)
        .IncludeInOpenApi();

        app.MapPost("/UserInGroup/{username}", async (string username, IsUserInGroupRequest request, HttpContext ctx) =>
            await ctx.ExecHandler(settings.Cache.CacheTimespan, () =>
            {
                (bool Belongs, IEnumerable<string> Groups) = repository.IsUserInGroups(username, request.Groups);

                return new IsUserInGroupResponse()
                {
                    Belongs = Belongs,
                    Groups = Groups.Select(x => new UserGroup() { GroupName = x })
                };
            })
        )
        .Produces<IsUserInGroupResponse>(200)
        .Produces<FailedResponse>(500)
        .Accepts<IsUserInGroupRequest>(ApplicationJson)
        .WithName("UserInGroup")
        .WithTags(ModuleTag)
        .IncludeInOpenApi();

        app.MapPost("/AuthenticateUser/{username}", async (string username, AuthenticUserRequest userRequest, HttpContext ctx) =>
        {
            await ctx.ExecHandler(userRequest, (userRequest) =>
            {
                (bool IsValid, string Message) = repository.AuthenticateUser(username, userRequest.Password);

                return new AuthenticUserResponse()
                {
                    IsValid = IsValid,
                    Message = Message
                };
            });
        })
        .Produces<AuthenticUserResponse>(200)
        .Produces<IEnumerable<ModelError>>(422)
        .Produces<FailedResponse>(500)
        .Accepts<AuthenticUserRequest>(ApplicationJson)
        .WithName("AuthenticateUser")
        .WithTags(ModuleTag)
        .IncludeInOpenApi();
    }
}
