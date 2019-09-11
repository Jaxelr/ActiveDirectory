using ActiveDirectory.Extensions;
using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules
{
    public class GroupModule : CarterModule
    {
        public GroupModule(IAdRepository repository, Store store)
        {
            Get<GetGroupUsers>("/GroupUser/{group}", (req, res, routeData) =>
            {
                string group = routeData.As<string>("group");

                return res.ExecHandler(group, store, () =>
                {
                    return repository.GetGroupUsers(group);
                });
            });
        }
    }
}
