using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Domain.Attendees;

internal sealed class AttendeeCheckedInDomainEvent : DomainEvent
{
    public Guid AttendeeId { get; init; }
    public Guid EventId { get; init; }

    public AttendeeCheckedInDomainEvent(Guid attendeeId, Guid eventId)
    {
        AttendeeId = attendeeId;
        EventId = eventId;
    }
}
