using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Events;

public sealed class EventCanceled(Guid eventId) : DomainEvent
{
    public Guid EventId { get; } = eventId;
}
