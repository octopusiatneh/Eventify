using Eventify.Modules.Events.Domain.Abstractions;

namespace Eventify.Modules.Events.Domain.Events;

public sealed class EventCreated : DomainEvent
{
    public Guid EventId { get; init; }

    public EventCreated(Guid eventId)
    {
        EventId = eventId;
    }
}
