using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Carts.Get;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
