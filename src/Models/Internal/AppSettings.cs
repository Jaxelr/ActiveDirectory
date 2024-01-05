using System.Collections.Generic;

namespace ActiveDirectory.Models.Internal;

public record AppSettings
{
    public CacheConfig Cache { get; init; }
    public RouteDefinition RouteDefinition { get; init; }
    public ICollection<string> Domains { get; init; }
    public ICollection<string> Addresses { get; init; }
}
