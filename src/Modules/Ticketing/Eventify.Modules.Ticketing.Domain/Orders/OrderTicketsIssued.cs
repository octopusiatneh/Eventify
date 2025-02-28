using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Orders;

public sealed class OrderTicketsIssued(Guid orderTicketId): DomainEvent
{
    public Guid OrderTicketId { get; } = orderTicketId;
}
