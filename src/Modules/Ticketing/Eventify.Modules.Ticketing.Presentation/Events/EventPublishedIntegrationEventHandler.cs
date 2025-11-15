using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.Events.Create;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Events;

public sealed class EventPublishedIntegrationEventHandler(ISender sender) : IConsumer<EventPublishedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<EventPublishedIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(
            new CreateEventCommand(
                message.EventId,
                message.Title,
                message.Description,
                message.Location,
                message.StartsAtUtc,
                message.EndsAtUtc,
                message.TicketTypes.ConvertAll(
                    x => new CreateEventCommand.TicketTypeRequest(
                        x.Id,
                        x.EventId,
                        x.Name,
                        x.Price,
                        x.Currency,
                        x.Quantity
                    )
                )
            )
        );
    }
}
