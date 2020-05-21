using System.Collections.Generic;

namespace ActiveDirectory.Entities.Operations
{
    public class IsUserInGroupResponse
    {
        public bool Belongs { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
