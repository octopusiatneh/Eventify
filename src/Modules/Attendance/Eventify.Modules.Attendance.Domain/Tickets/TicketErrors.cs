using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Domain.Tickets;

public static class TicketErrors
{
    public static Error NotFound(Guid ticketId) =>
        Error.NotFound("Tickets.NotFound", $"The ticket with the identifier {ticketId} was not found");

    public static Error NotFound(string code) =>
        Error.NotFound("Tickets.NotFound", $"The ticket with the code {code} was not found");

    public static readonly Error AlreadyUsed = Error.Problem(
        "Tickets.AlreadyUsed",
        "The ticket has already been used");

    public static readonly Error InvalidCheckIn = Error.Problem(
        "Tickets.InvalidCheckIn",
        "The ticket check in was invalid");

    public static readonly Error DuplicateCheckIn = Error.Problem(
        "Tickets.DuplicateCheckIn",
        "The ticket was already checked in");

    public static readonly Error InvalidForAttendee = Error.Problem(
        "Tickets.InvalidForAttendee",
        "The ticket does not belong to this attendee");
}
