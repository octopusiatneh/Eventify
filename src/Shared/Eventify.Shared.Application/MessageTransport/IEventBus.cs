namespace Eventify.Shared.Application.MessageTransport;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationMessage;
}
