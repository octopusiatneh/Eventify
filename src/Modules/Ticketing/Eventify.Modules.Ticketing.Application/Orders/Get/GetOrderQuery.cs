using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Orders.Get;

public sealed record GetOrderQuery(Guid OrderId) : IQuery<OrderResponse>;
