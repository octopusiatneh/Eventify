using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Orders;

internal sealed class OrderCreated(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; } = orderId;
}
