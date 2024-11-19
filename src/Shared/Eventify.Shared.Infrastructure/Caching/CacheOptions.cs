using Microsoft.Extensions.Caching.Distributed;

namespace Eventify.Shared.Infrastructure.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultOptions => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
        => expiration is null
            ? DefaultOptions
            : new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
}
