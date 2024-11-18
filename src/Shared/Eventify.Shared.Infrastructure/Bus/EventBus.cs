using Eventify.Shared.Application.EventBus;
using MassTransit;

namespace Eventify.Shared.Infrastructure.Bus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationMessage
        => await bus.Publish(message, cancellationToken);
}
