using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Tickets;

public sealed class TicketCreated(Guid ticketId) : DomainEvent
{
    public Guid TicketId { get; init; } = ticketId;
}
