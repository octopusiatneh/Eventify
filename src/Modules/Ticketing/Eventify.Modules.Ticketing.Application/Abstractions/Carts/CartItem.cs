namespace Eventify.Modules.Ticketing.Application.Abstractions.Carts;

public sealed record CartItem(Guid TicketTypeId, int Quantity);
