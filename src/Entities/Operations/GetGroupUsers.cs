using System.Collections.Generic;

namespace ActiveDirectory
{
    public class GetGroupUsersRequest
    {
        public string Group { get; set; }
    }

    public class GetGroupUsersResponse
    {
        public IEnumerable<User> Response { get; set; }
    }
}
