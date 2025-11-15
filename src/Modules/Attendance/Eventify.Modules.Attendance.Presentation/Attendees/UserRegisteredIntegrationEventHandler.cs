using Eventify.Modules.Attendance.Application.Attendees.Create;
using Eventify.Modules.Users.IntegrationEvents;
using Eventify.Shared.Application.Exceptions;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Attendees;

public sealed class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await sender.Send(
            new CreateAttendeeCommand(
                message.UserId,
                message.Email,
                message.FirstName,
                message.LastName));

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateAttendeeCommand), result.Error);
        }
    }
}
