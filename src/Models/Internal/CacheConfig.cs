namespace ActiveDirectory.Models.Internal;

public record CacheConfig
{
    public bool CacheEnabled { get; init; }
    public int CacheTimespan { get; init; }
    public int CacheMaxSize { get; init; }
}
