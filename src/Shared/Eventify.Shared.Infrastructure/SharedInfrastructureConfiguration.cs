using Eventify.Shared.Application.Bus;
using Eventify.Shared.Application.Caching;
using Eventify.Shared.Application.Clock;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Infrastructure.Bus;
using Eventify.Shared.Infrastructure.Caching;
using Eventify.Shared.Infrastructure.Clock;
using Eventify.Shared.Infrastructure.Data;
using Eventify.Shared.Infrastructure.Interceptors;
using Eventify.Shared.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace Eventify.Shared.Infrastructure;

public static class SharedInfrastructureConfiguration
{
    public static IServiceCollection AddSharedInfrastructureConfig(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.Configure<DbConnectionStringOptions>(
            opts => configuration.GetSection(DbConnectionStringOptions.DbConnectionString)
        );
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.TryAddSingleton<PublishDomainEventInterceptor>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.TryAddSingleton<IEventBus, EventBus>();
        services.AddMassTransit(configure =>
        {
            Array.ForEach(moduleConfigureConsumers, configureConsumer => configureConsumer(configure));
            configure.SetKebabCaseEndpointNameFormatter();
            configure.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
        });

        RegisterPostgres(configuration, services);
        RegisterCaching(configuration, services);

        return services;
    }

    private static void RegisterPostgres(IConfiguration configuration, IServiceCollection services)
    {
        var dbConnectionString = configuration.GetConnectionString("Database")!;
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
    }

    private static void RegisterCaching(IConfiguration configuration, IServiceCollection services)
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
