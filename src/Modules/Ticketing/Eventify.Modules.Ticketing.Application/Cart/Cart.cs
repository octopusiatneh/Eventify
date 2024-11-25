namespace Eventify.Modules.Ticketing.Application.Cart;

public sealed record Cart(Guid CustomerId, List<CartItem> Items)
{
    internal static Cart InitEmptyCartForCustomer(Guid customerId) => new(customerId, []);
}
