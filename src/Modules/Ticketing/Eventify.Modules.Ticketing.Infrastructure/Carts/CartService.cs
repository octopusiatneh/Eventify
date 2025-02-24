using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Application.Carts.DTOs;
using Eventify.Shared.Application.Caching;

namespace Eventify.Modules.Ticketing.Infrastructure.Carts;

public sealed class CartService(ICacheService cacheService) : ICartService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(20);

    public async Task AddItemAsync(Guid customerId, CartItem cartItem, CancellationToken cancellationToken = default)
    {
        var cacheKey = CreateCacheKey(customerId);
        var cart = await GetAsync(customerId, cancellationToken);
        var existingCartItemIndex = cart.Items.FindIndex(c => c.TicketTypeId == cartItem.TicketTypeId);
        var isCartItemExisted = existingCartItemIndex is not -1;

        if (isCartItemExisted)
        {
            var existingCartItem = cart.Items[existingCartItemIndex];
            cart.Items[existingCartItemIndex] = existingCartItem with { Quantity = existingCartItem.Quantity + cartItem.Quantity };
        }
        else
        {
            cart.Items.Add(cartItem);
        }

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    public async Task<Cart> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var cacheKey = CreateCacheKey(customerId);
        var cart = await cacheService.GetAsync<Cart>(cacheKey, cancellationToken)
            ?? Cart.InitEmptyCartForCustomer(customerId);

        return cart;
    }

    public async Task ClearAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var cacheKey = CreateCacheKey(customerId);
        var cart = Cart.InitEmptyCartForCustomer(customerId);

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    public async Task RemoveItemAsync(Guid customerId, Guid ticketTypeId, CancellationToken cancellationToken = default)
    {
        var cacheKey = CreateCacheKey(customerId);

        Cart cart = await GetAsync(customerId, cancellationToken);

        CartItem? cartItem = cart.Items.Find(c => c.TicketTypeId == ticketTypeId);

        if (cartItem is null)
        {
            return;
        }

        cart.Items.Remove(cartItem);

        await cacheService.SetAsync(cacheKey, cart, DefaultExpiration, cancellationToken);
    }

    private static string CreateCacheKey(Guid customerId) => $"carts:{customerId}";
}
