using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Domain.Tickets;

internal sealed class TicketUsedDomainEvent : DomainEvent
{
    public Guid TicketId { get; init; }

    public TicketUsedDomainEvent(Guid ticketId)
    {
        TicketId = ticketId;
    }
}
