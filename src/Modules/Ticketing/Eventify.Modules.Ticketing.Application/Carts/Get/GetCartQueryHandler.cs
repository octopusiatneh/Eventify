using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.Exceptions;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Carts.Get;

internal sealed class GetCartQueryHandler(
    ICustomerRepository customerRepository,
    ICartService cartService) : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;

        var customer = await customerRepository.GetAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure<Cart>(CustomerErrors.NotFound(customerId));
        }

        var cart = await cartService.GetAsync(customerId, cancellationToken);
        if (cart is null)
        {
            throw new EventifyException(nameof(GetCartQuery));
        }

        return cart;
    }
}
