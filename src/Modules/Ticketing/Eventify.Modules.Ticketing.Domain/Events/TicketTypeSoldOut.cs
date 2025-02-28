using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Events;

public sealed class TicketTypeSoldOut(Guid ticketTypeId) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;
}
