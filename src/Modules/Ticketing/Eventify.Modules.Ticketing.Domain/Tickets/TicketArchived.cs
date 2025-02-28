using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Tickets;

public sealed class TicketArchived(Guid ticketId, string code) : DomainEvent
{
    public Guid TicketId { get; init; } = ticketId;

    public string Code { get; init; } = code;
}
