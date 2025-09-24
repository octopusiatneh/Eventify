using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Orders;

public sealed class OrderTicketsIssuedDomainEvent(Guid orderTicketId) : DomainEvent
{
    public Guid OrderTicketId { get; } = orderTicketId;
}
