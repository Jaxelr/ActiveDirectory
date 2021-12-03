using ActiveDirectory.Extensions;
using ActiveDirectory.Models.Internal;
using ActiveDirectory.Repositories;
using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules;

public class GroupModule : CarterModule
{
    public GroupModule(IAdRepository repository, AppSettings settings)
    {
        Get<GetGroupUsers>("/GroupUser/{group}", (req, res) =>
        {
            string group = req.RouteValues.As<string>("group");

            return res.ExecHandler(settings.Cache.CacheTimespan, () => repository.GetGroupUsers(group));
        });
    }
}
