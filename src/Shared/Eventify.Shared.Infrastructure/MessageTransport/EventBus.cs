using Eventify.Shared.Application.MessageTransport;
using MassTransit;

namespace Eventify.Shared.Infrastructure.MessageTransport;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationMessage
        => await bus.Publish(message, cancellationToken);
}
