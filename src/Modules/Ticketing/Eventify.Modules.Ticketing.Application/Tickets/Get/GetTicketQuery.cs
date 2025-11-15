using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Tickets.Get;

public sealed record GetTicketQuery(Guid TicketId) : IQuery<TicketResponse>;
