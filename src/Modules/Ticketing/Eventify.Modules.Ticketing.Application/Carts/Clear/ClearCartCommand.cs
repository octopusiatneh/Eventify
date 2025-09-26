using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Carts.Clear;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;

