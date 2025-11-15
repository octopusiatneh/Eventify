using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Attendance.Application.Attendees.Create;

public sealed record CreateAttendeeCommand(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName) : ICommand;
