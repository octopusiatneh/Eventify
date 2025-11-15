using Eventify.Modules.Attendance.Application.Tickets.Create;
using Eventify.Modules.Ticketing.IntegrationEvents;
using Eventify.Shared.Application.Exceptions;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Tickets;

public sealed class TicketIssuedIntegrationEventHandler(ISender sender) : IConsumer<TicketIssuedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TicketIssuedIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await sender.Send(
            new CreateTicketCommand(
                message.TicketId,
                message.CustomerId,
                message.EventId,
                message.Code));

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateTicketCommand), result.Error);
        }
    }
}
