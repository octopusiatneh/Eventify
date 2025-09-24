using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Tickets.ArchiveForEvent;

public sealed record ArchiveTicketCommand(Guid EventId) : ICommand;
