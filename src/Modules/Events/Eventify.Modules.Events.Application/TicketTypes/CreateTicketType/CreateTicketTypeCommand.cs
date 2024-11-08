using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.TicketTypes.CreateTicketType;

public sealed record CreateTicketTypeCommand(
    Guid EventId,
    string Name,
    decimal Price,
    string Currency,
    int Quantity) : ICommand<Guid>;
