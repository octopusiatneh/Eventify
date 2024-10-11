namespace Eventify.Modules.Events.Domain.Abstractions;

public interface IDomainEvent
{
    public Guid Id { get; }

    public DateTime OccurredOnUtc { get; }
}
