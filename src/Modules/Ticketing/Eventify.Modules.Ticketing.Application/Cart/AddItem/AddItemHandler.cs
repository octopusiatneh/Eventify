using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Cart.AddItem;

internal sealed class AddItemHandler(CartService cartService) : ICommandHandler<AddItemCommand>
{
    public async Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var (customerId, ticketTypeId, quantity) = request;
        await cartService.AddItemAsync(customerId, new CartItem(ticketTypeId, quantity), cancellationToken);

        return Result.Success();
    }
}
