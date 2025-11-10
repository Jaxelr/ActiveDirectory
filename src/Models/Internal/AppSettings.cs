using System.Collections.Generic;

namespace ActiveDirectory.Models.Internal;

public record AppSettings
{
    public CacheConfig Cache { get; init; } = new();
    public RouteDefinition RouteDefinition { get; init; } = new();
    public ICollection<string> Domains { get; init; } = [];
    public ICollection<string> Addresses { get; init; } = [];
}
