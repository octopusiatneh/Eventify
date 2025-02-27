namespace Eventify.Modules.Ticketing.Application.Abstractions.Carts;

public sealed record Cart(Guid CustomerId, List<CartItem> Items)
{
    public static Cart InitEmptyCartForCustomer(Guid customerId) => new(customerId, []);
}
