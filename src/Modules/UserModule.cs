using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules
{
    public class UserModule : CarterModule
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IAdRepository repository;
        private readonly Store store;
#pragma warning restore IDE0052 // Remove unread private members

        public UserModule(IAdRepository repository, Store store)
        {
            this.repository = repository;
            this.store = store;

            Get<GetUserGroups>("/UserGroups/{username}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");

                return res.ExecHandler(username, store, () =>
                {
                    return repository.GetUserGroups(username);
                });
            });

            Get<GetUser>("/User/{username}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");

                return res.ExecHandler(username, store, () =>
                {
                    return repository.GetUserInfo(username);
                });
            });

            Get<GetAuthenticateUser>("/AuthenticateUser/{username}/{password}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");
                string password = routeData.As<string>("password");

                return res.ExecHandler(() =>
                {
                    return repository.AuthenticateUser(username, password);
                });
            });

            Get<GetIsUserInGroup>("/UserInGroup/{username}/{groups}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");
                string[] groups = routeData.As<string>("groups").Split(',');

                string key = string.Concat(username, groups);

                return res.ExecHandler(key, store, () =>
                {
                    return repository.IsUserInGroups(username, groups);
                });
            });
        }
    }
}
