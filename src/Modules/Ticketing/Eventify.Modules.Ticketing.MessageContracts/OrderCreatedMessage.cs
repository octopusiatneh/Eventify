using Eventify.Shared.Application.MessageTransport;

namespace Eventify.Modules.Ticketing.IntegrationEvent;

public sealed record OrderCreatedMessage(
    Guid Id,
    Guid OrderId,
    Guid CustomerId,
    decimal TotalPrice,
    DateTime CreatedAtUtc,
    DateTime OccurredOnUtc,
    List<OrderItemModel> OrderItems) : IntegrationMessage(Id, OccurredOnUtc);
