using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.TicketTypes;

public sealed class TicketTypeSoldOutDomainEvent(Guid ticketTypeId) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;
}
