﻿namespace Eventify.Modules.Ticketing.Application.Abstractions.Carts;

public interface ICartService
{
    Task AddItemAsync(Guid customerId, CartItem cartItem, CancellationToken cancellationToken = default);

    Task<Cart> GetAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task ClearAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task RemoveItemAsync(Guid customerId, Guid ticketTypeId, CancellationToken cancellationToken = default);
}
