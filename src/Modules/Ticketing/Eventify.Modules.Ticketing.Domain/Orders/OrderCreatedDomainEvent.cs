using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Orders;

internal sealed class OrderCreatedDomainEvent(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; } = orderId;
}
