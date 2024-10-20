using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.Events;

public sealed class EventCreated : DomainEvent
{
    public Guid EventId { get; init; }

    public EventCreated(Guid eventId)
    {
        EventId = eventId;
    }
}
