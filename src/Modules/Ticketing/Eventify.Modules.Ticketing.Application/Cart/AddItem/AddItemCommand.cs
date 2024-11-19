using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Cart.AddItem;

public sealed record AddItemCommand(Guid EventId, Guid TicketTypeId, int Quantity) : ICommand;
