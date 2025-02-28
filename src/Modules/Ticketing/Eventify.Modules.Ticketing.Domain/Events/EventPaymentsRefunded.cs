using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Events;

public sealed class EventPaymentsRefunded(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
