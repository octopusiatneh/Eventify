using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Carts.RemoveItem;

public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;
