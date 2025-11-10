namespace ActiveDirectory.Models.Internal;

public record RouteDefinition
{
    public string RouteSuffix { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
}
