namespace Eventify.Modules.Ticketing.Application.Cart;

public sealed class CartItem
{
    public Guid TicketTypeId { get; init; }
    
    public string Currenncy { get; init; }

    public string Quantity { get; init; }

    public decimal Price { get; init; }
}
