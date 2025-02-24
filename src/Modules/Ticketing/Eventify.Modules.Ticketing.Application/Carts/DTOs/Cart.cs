namespace Eventify.Modules.Ticketing.Application.Carts.DTOs;

public sealed record Cart(Guid CustomerId, List<CartItem> Items)
{
    public static Cart InitEmptyCartForCustomer(Guid customerId) => new(customerId, []);
}
