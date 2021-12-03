using System.Collections.Generic;
using ActiveDirectory.Models.Entities;

namespace ActiveDirectory.Repositories;

public interface IAdRepository
{
    (bool, string) AuthenticateUser(string userName, string password, string domain);

    (bool, string) AuthenticateUser(string userName, string password);

    IEnumerable<UserGroup> GetUserGroups(string userName);

    IEnumerable<User> GetGroupUsers(string group);

    IEnumerable<User> GetGroupUsers(IEnumerable<string> groups);

    User GetUserInfo(string userName);

    (bool, IEnumerable<string>) IsUserInGroups(string userName, IEnumerable<string> groups);
}
