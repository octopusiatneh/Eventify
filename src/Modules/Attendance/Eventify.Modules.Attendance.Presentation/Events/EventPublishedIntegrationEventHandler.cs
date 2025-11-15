using Eventify.Modules.Attendance.Application.Events.Create;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Shared.Application.Exceptions;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Events;

public sealed class EventPublishedIntegrationEventHandler(ISender sender) : IConsumer<EventPublishedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<EventPublishedIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await sender.Send(
            new CreateEventCommand(
                message.EventId,
                message.Title,
                message.Description,
                message.Location,
                message.StartsAtUtc,
                message.EndsAtUtc));

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateEventCommand), result.Error);
        }
    }
}
