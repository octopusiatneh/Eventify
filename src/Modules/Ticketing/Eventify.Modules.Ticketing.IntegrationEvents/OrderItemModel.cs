namespace Eventify.Modules.Ticketing.IntegrationEvents;

public sealed record OrderItemModel(
    Guid Id,
    Guid OrderId,
    Guid TicketTypeId,
    decimal Quantity,
    decimal UnitPrice,
    decimal Price,
    string Currency);
