namespace Eventify.Shared.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IIntegrationMessage;
}

public interface IIntegrationMessage
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}

public abstract class IntegrationMessage : IIntegrationMessage
{
    protected IntegrationMessage(Guid id, DateTime occurredOnUtc)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
    }

    public Guid Id { get; init; }
    public DateTime OccurredOnUtc { get; init; }
}
