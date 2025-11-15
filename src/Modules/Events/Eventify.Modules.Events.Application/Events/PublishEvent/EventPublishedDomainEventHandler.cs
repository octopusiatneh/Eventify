using Eventify.Modules.Events.Application.Events.Get;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.EventBus;
using MediatR;

namespace Eventify.Modules.Events.Application.Events.PublishEvent;

internal sealed class EventPublishedDomainEventHandler(
    ISender sender,
    IEventBus eventBus)
    : IDomainEventHandler<EventPublishedDomainEvent>
{
    public async Task Handle(EventPublishedDomainEvent notification, CancellationToken cancellationToken)
    {
        var getEventQuery = new GetEventQuery(notification.EventId);
        var getEventResult = await sender.Send(getEventQuery, cancellationToken);

        if (getEventResult.IsFailure)
        {
            return;
        }

        var @event = getEventResult.Value;

        var integrationEvent = new EventPublishedIntegrationEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            @event.Id,
            @event.Title,
            @event.Description,
            @event.Location,
            @event.StartsAtUtc,
            @event.EndsAtUtc,
            @event.TicketTypes.ConvertAll(t => new TicketTypeModel
            {
                Id = t.TicketTypeId,
                EventId = @event.Id,
                Name = t.Name,
                Price = t.Price,
                Currency = t.Currency,
                Quantity = t.Quantity
            })
        );

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
