using System.Collections.Generic;
using System.Linq;
using ActiveDirectory.Models.Entities;
using ActiveDirectory.Repositories;
using ActiveDirectoryTests.Fakes;

namespace ActiveDirectoryTests
{
    public class MockAdRepository : IAdRepository
    {
        private const string LoggedMessage = "Logged.";

        public (bool, string) AuthenticateUser(string userName, string password, string domain) => (true, LoggedMessage);

        public (bool, string) AuthenticateUser(string userName, string password) => (true, LoggedMessage);

        public IEnumerable<User> GetGroupUsers(string group) => new List<User>() { new FakeUser(group: group) };

        public IEnumerable<User> GetGroupUsers(IEnumerable<string> groups) => groups.Select(x => new FakeUser(group: x));

        public IEnumerable<UserGroup> GetUserGroups(string userName) => new List<UserGroup> { new FakeUserGroup() };

        public User GetUserInfo(string userName) => new FakeUser(userName: userName);

        public (bool, IEnumerable<string>) IsUserInGroups(string userName, IEnumerable<string> groups) =>
            (true, new List<string>() { new FakeUserGroup().GroupName });
    }
}
