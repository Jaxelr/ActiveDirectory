using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveDirectory.Modules;

public class UserModule : ICarterModule
{
    //public UserModule(IAdRepository repository, AppSettings settings)
    //{
    //    Get<GetUser>("/User/{username}", (req, res) =>
    //    {
    //        string username = req.RouteValues.As<string>("username");

    //        return res.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetUserInfo(username));
    //    });

    //    Get<GetIsUserInGroup>("/UserInGroup/{username}/{groups}", (req, res) =>
    //    {
    //        string username = req.RouteValues.As<string>("username");
    //        string[] groups = req.RouteValues.As<string>("groups").Split(',');

    //        string key = string.Concat(username, groups);

    //        return res.ExecHandler(settings.Cache.CacheTimespan, () =>
    //        {
    //            (bool Belongs, IEnumerable<string> Groups) = repository.IsUserInGroups(username, groups);

    //            return new IsUserInGroupResponse()
    //            {
    //                Belongs = Belongs,
    //                Groups = Groups.Select(x => new UserGroup() { GroupName = x })
    //            };
    //        });
    //    });

    //    Post<PostAuthenticateUser>("/AuthenticateUser/{username}", (req, res) =>
    //    {
    //        string username = req.RouteValues.As<string>("username");

    //        return res.ExecHandler<AuthenticUserRequest, AuthenticUserResponse>(req, (userRequest) =>
    //        {
    //            (bool IsValid, string Message) = repository.AuthenticateUser(username, userRequest.Password);

    //            return new AuthenticUserResponse()
    //            {
    //                IsValid = IsValid,
    //                Message = Message
    //            };
    //        });
    //    });
    //}

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var repository = app.ServiceProvider.GetRequiredService<IAdRepository>();
        var settings = app.ServiceProvider.GetRequiredService<AppSettings>();

        app.MapGet("/UserGroup/{username}", (string username, HttpResponse res) =>
            res.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetUserGroups(username))
        );
    }
}
