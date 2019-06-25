using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules
{
    public class GroupModule : CarterModule
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IAdRepository repository;
        private readonly Store store;
#pragma warning restore IDE0052 // Remove unread private members

        public GroupModule(IAdRepository repository, Store store)
        {
            this.repository = repository;
            this.store = store;

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
