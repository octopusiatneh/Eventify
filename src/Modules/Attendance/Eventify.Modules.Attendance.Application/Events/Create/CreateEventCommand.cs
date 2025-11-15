using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Attendance.Application.Events.Create;

public sealed record CreateEventCommand(
    Guid EventId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand;
