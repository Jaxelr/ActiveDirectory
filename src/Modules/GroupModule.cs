using ActiveDirectory.Extensions;
using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules
{
    public class GroupModule : CarterModule
    {
        public GroupModule(IAdRepository repository, Store store)
        {
            Get<GetGroupUsers>("/GroupUser/{group}", (req, res) =>
            {
                string group = req.RouteValues.As<string>("group");

                string key = Key.Create<GetGroupUsers>(group);

                return res.ExecHandler(key, store, () => repository.GetGroupUsers(group));
            });
        }
    }
}
