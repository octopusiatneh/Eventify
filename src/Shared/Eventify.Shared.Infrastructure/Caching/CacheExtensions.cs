using Eventify.Shared.Application.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Eventify.Shared.Infrastructure.Caching;

internal static class CacheExtensions
{
    internal static void AddRedisCaching(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var redisConnectionString = configuration.GetConnectionString("Redis")!;
            var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory =
                    () => Task.FromResult<IConnectionMultiplexer>(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();
    }
}
