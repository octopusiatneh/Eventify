namespace Eventify.Shared.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}
