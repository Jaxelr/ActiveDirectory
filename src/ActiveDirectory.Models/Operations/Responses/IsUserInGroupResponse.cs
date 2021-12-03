using System.Collections.Generic;
using ActiveDirectory.Models.Entities;

namespace ActiveDirectory.Models.Operations;

public class IsUserInGroupResponse
{
    public bool Belongs { get; set; }
    public IEnumerable<UserGroup> Groups { get; set; }
}
