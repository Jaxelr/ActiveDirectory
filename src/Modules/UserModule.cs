using System.Collections.Generic;
using ActiveDirectory.Entities.Operations;
using ActiveDirectory.Extensions;
using Carter;
using Carter.Request;

namespace ActiveDirectory.Modules
{
    public class UserModule : CarterModule
    {
        public UserModule(IAdRepository repository, Store store)
        {
            Get<GetUserGroups>("/UserGroup/{username}", (req, res, routeData) =>
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

            Get<GetIsUserInGroup>("/UserInGroup/{username}/{groups}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");
                string[] groups = routeData.As<string>("groups").Split(',');

                string key = string.Concat(username, groups);

                return res.ExecHandler(key, store, () =>
                {
                    (bool Belongs, IEnumerable<string> Groups) = repository.IsUserInGroups(username, groups);

                    return new IsUserInGroupResponse()
                    {
                        Belongs = Belongs,
                        Groups = Groups
                    };
                });
            });

            Post<PostAuthenticateUser>("/AuthenticateUser/{username}", (req, res, routeData) =>
            {
                string username = routeData.As<string>("username");

                return res.ExecHandler<AuthenticUserRequest, AuthenticUserResponse>(req, (userRequest) =>
                {
                    (bool IsValid, string Message) = repository.AuthenticateUser(username, userRequest.Password);

                    return new AuthenticUserResponse()
                    {
                        IsValid = IsValid,
                        Message = Message
                    };
                });
            });
        }
    }
}
