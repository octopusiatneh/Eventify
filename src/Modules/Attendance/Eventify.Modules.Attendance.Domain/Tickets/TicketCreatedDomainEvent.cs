using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Domain.Tickets;

internal sealed class TicketCreatedDomainEvent : DomainEvent
{
    public Guid TicketId { get; init; }
    public Guid EventId { get; init; }

    public TicketCreatedDomainEvent(Guid ticketId, Guid eventId)
    {
        TicketId = ticketId;
        EventId = eventId;
    }
}
