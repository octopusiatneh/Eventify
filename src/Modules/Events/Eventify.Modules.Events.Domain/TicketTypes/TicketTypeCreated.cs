using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.TicketTypes;

public sealed class TicketTypeCreated(Guid ticketTypeId) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;
}
