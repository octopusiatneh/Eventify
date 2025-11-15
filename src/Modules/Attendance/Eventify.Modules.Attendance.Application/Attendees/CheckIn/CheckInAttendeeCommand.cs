using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Attendance.Application.Attendees.CheckIn;

public sealed record CheckInAttendeeCommand(Guid AttendeeId, Guid TicketId) : ICommand;
