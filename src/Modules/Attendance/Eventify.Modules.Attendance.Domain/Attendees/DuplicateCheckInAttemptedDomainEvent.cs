using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Domain.Attendees;

internal sealed class DuplicateCheckInAttemptedDomainEvent : DomainEvent
{
    public Guid AttendeeId { get; init; }
    public Guid EventId { get; init; }
    public Guid TicketId { get; init; }
    public string TicketCode { get; init; }

    public DuplicateCheckInAttemptedDomainEvent(Guid attendeeId, Guid eventId, Guid ticketId, string ticketCode)
    {
        AttendeeId = attendeeId;
        EventId = eventId;
        TicketId = ticketId;
        TicketCode = ticketCode;
    }
}
