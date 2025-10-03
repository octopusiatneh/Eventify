using Eventify.Shared.Application.EventBus;
using MassTransit;

namespace Eventify.Shared.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
        => await bus.Publish(message, cancellationToken);
}
