using ActiveDirectory.Models.Entities;

namespace ActiveDirectoryTests.Fakes
{
    public class FakeUserGroup : UserGroup
    {
        internal const string DefaultGroupName = "Default Group";

        public FakeUserGroup(string groupName = DefaultGroupName)
        {
            GroupName = groupName;
        }
    }
}
