using ActiveDirectory.Models.Entities;

namespace ActiveDirectoryTests.Fakes
{
    public class FakeUser : User
    {
        public static string Password => "password";

        internal const string DefaultDisplayName = "John Doe";
        internal const string DefaultEmail = "mail@mail.com";
        internal const string DefaultGroup = "Default Group";
        internal const string DefaultUsername = "jdoe";

        public FakeUser(string userName = DefaultUsername, string group = DefaultGroup) : this()
        {
            UserName = userName;
            Group = group;
        }

        public FakeUser()
        {
            UserName = DefaultUsername;
            DisplayName = DefaultDisplayName;
            Email = DefaultEmail;
            Group = DefaultGroup;
        }
    }
}
