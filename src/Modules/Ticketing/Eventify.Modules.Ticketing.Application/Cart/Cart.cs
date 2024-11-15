﻿namespace Eventify.Modules.Ticketing.Application.Cart;

public sealed class Cart
{
    public Guid CustomerId { get; init; }

    public List<CartItem> CartItems { get; init; }

    internal static Cart Create(Guid customerId) => new() { CustomerId = customerId };
}
