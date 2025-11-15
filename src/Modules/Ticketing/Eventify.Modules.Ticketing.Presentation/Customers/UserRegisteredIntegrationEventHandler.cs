using Eventify.Modules.Ticketing.Application.Customers.Create;
using Eventify.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

public sealed class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(
            new CreateCustomerCommand(message.UserId, message.Email, message.FirstName, message.LastName)
        );
    }
}
