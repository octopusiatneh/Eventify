namespace Eventify.Modules.Ticketing.Application.Carts.DTOs;

public sealed record CartItem(Guid TicketTypeId, int Quantity);
