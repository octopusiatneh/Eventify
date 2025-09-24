using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Orders.Create;

internal sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
