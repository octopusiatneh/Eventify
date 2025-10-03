using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Orders.Create;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
