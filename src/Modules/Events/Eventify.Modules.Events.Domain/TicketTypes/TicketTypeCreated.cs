using Eventify.Modules.Events.Domain.Abstractions;

namespace Eventify.Modules.Events.Domain.TicketTypes;

public sealed class TicketTypeCreated(Guid ticketTypeId, decimal price) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;

    public decimal Price { get; init; } = price;
}
