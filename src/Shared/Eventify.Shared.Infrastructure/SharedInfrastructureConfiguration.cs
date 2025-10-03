using Eventify.Shared.Application.Clock;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Application.EventBus;
using Eventify.Shared.Infrastructure.Authentication;
using Eventify.Shared.Infrastructure.Authorization;
using Eventify.Shared.Infrastructure.Caching;
using Eventify.Shared.Infrastructure.Clock;
using Eventify.Shared.Infrastructure.Database;
using Eventify.Shared.Infrastructure.EventBus;
using Eventify.Shared.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Eventify.Shared.Infrastructure;

public static class SharedInfrastructureConfiguration
{
    public static IServiceCollection AddSharedInfrastructureConfig(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddEventifyAuthentication();
        services.AddEventifyAuthorization();

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventInterceptor>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddPostgres(configuration);
        services.AddRedisCaching(configuration);
        services.AddMassTransit(moduleConfigureConsumers);

        return services;
    }
}
