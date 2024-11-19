using MediatR;

namespace Eventify.Shared.Domain;

public interface IDomainEvent : INotification
{
    public Guid Id { get; }

    public DateTime OccurredOnUtc { get; }
}
