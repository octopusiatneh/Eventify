using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Carts.Clear;

internal sealed class ClearCartCommandHandler(
    ICustomerRepository customerRepository,
    ICartService cartService) : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;

        var customer = await customerRepository.GetAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(customerId));
        }

        await cartService.ClearAsync(customerId, cancellationToken);

        return Result.Success();
    }
}

