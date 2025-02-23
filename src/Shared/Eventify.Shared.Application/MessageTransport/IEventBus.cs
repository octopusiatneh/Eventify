namespace Eventify.Shared.Application.MessageTransport;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IIntegrationMessage;
}

public interface IIntegrationMessage
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}

public abstract record IntegrationMessage(Guid Id, DateTime OccurredOnUtc) : IIntegrationMessage;
