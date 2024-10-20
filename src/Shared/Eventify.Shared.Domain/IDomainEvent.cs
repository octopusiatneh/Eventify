namespace Eventify.Shared.Domain;

public interface IDomainEvent
{
    public Guid Id { get; }

    public DateTime OccurredOnUtc { get; }
}
