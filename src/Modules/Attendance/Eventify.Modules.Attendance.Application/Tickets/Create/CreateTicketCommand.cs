using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Attendance.Application.Tickets.Create;

public sealed record CreateTicketCommand(
    Guid TicketId,
    Guid CustomerId,
    Guid EventId,
    string Code) : ICommand;
