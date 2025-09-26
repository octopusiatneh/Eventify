using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Domain.TicketTypes;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Carts.RemoveItem;

internal sealed class RemoveItemFromCartCommandHandler(
    ICustomerRepository customerRepository,
    ITicketTypeRepository ticketTypeRepository,
    ICartService cartService) : ICommandHandler<RemoveItemFromCartCommand>
{
    public async Task<Result> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var (customerId, ticketTypeId) = request;

        var getCustomerTask = customerRepository.GetAsync(customerId, cancellationToken);
        var getTicketTypeTask = ticketTypeRepository.GetAsync(ticketTypeId, cancellationToken);
        await Task.WhenAll(getCustomerTask, getTicketTypeTask);

        var customer = await getCustomerTask;
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(customerId));
        }

        var ticketType = await getTicketTypeTask;
        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(ticketTypeId));
        }
        await cartService.RemoveItemAsync(customerId, ticketTypeId, cancellationToken);

        return Result.Success();
    }
}
