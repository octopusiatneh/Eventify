using Eventify.Modules.Ticketing.Application.Tickets.Get;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.IntegrationEvents;
using Eventify.Shared.Application.EventBus;
using Eventify.Shared.Application.Exceptions;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateBatch;

internal sealed class TicketCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : INotificationHandler<TicketCreatedDomainEvent>
{
    public async Task Handle(
        TicketCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTicketQuery(notification.TicketId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetTicketQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new TicketIssuedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.CustomerId,
                result.Value.EventId,
                result.Value.Code),
            cancellationToken);
    }
}
