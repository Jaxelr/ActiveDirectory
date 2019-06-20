using System;
using System.Threading.Tasks;
using Carter;
using Carter.Request;
using Carter.Response;
using Microsoft.Extensions.Caching.Memory;

namespace AdSample.Modules
{
    public class UserModule : CarterModule
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IAdRepository repository;
        private readonly IMemoryCache cache;
#pragma warning restore IDE0052 // Remove unread private members

        public UserModule(IAdRepository repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;

            Get<GetUserGroups>("/UserGroups/{username}", (req, res, routeData) =>
            {
                try
                {
                    string username = routeData.As<string>("username");

                    var response = repository.GetUserGroups(username);

                    if (response is null)
                    {
                        res.StatusCode = 204;
                        return Task.CompletedTask;
                    }

                    res.StatusCode = 200;
                    return res.Negotiate(response);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    return res.Negotiate(ex.Message);
                }
            });

            Get<GetUser>("/User/{username}", (req, res, routeData) =>
            {
                try
                {
                    string username = routeData.As<string>("username");

                    var response = repository.GetUserInfo(username);

                    if (response is null)
                    {
                        res.StatusCode = 204;
                        return Task.CompletedTask;
                    }

                    res.StatusCode = 200;
                    return res.Negotiate(response);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    return res.Negotiate(ex.Message);
                }
            });

            Get<GetAuthenticateUser>("/AuthenticateUser/{username}/{password}", (req, res, routeData) =>
            {
                try
                {
                    string username = routeData.As<string>("username");
                    string password = routeData.As<string>("password");

                    var response = repository.AuthenticateUser(username, password);

                    res.StatusCode = 200;
                    return res.Negotiate(response);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    return res.Negotiate(ex.Message);
                }
            });

            Get<GetIsUserInGroup>("UserInGroup/{username}/{groups}", (req, res, routeData) =>
            {
                try
                {
                    string username = routeData.As<string>("username");
                    string groups = routeData.As<string>("groups");

                    bool response = repository.IsUserInGroups(username, groups.Split(','));

                    res.StatusCode = 200;
                    return res.Negotiate(response);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500;
                    return res.Negotiate(ex.Message);
                }
            });
        }
    }
}
