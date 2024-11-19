using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.Events;

public sealed class EventRescheduledEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
