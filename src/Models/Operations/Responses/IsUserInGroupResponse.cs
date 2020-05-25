using System.Collections.Generic;

namespace ActiveDirectory.Models.Operations
{
    public class IsUserInGroupResponse
    {
        public bool Belongs { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}
