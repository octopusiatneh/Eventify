using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.Events;

public sealed class EventRescheduled(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
