using System;
using System.Threading.Tasks;
using Carter;
using Carter.Request;
using Carter.Response;
using Microsoft.Extensions.Caching.Memory;

namespace AdSample.Modules
{
    public class GroupModule : CarterModule
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IAdRepository repository;
        private readonly IMemoryCache cache;
#pragma warning restore IDE0052 // Remove unread private members

        public GroupModule(IAdRepository repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;

            Get<GetGroupUsers>("/GroupUser/{group}", (req, res, routeData) =>
            {
                try
                {
                    string group = routeData.As<string>("group");

                    string key = string.Concat(nameof(GetGroupUsers), ":", group);

                    if (cache.TryGetValue(key, out object obj))
                    {
                        res.StatusCode = 200;
                        return res.Negotiate(obj);
                    }

                    var response = repository.GetGroupUsers(group);

                    if (response is null)
                    {
                        res.StatusCode = 204;
                        return Task.CompletedTask;
                    }

                    cache.Set(key, response);

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
