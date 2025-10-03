using Eventify.Shared.Infrastructure.EventBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Infrastructure.EventBus;

public static class MessageTransportExtensions
{
    public static void AddMassTransit(this IServiceCollection services, Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddMassTransit(configure =>
        {
            Array.ForEach(moduleConfigureConsumers, configureConsumer => configureConsumer(configure));
            configure.SetSnakeCaseEndpointNameFormatter();
            configure.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
        });
    }
}
