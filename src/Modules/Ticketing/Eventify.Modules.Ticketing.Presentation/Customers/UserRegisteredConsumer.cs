using Eventify.Modules.Ticketing.Application.Customers.Create;
using Eventify.Modules.Users.MessageContracts.IntegrationMessages;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

public sealed class UserRegisteredConsumer(ISender sender) : IConsumer<UserRegisteredMessage>
{
    public async Task Consume(ConsumeContext<UserRegisteredMessage> context)
    {
        var message = context.Message;
        await sender.Send(new CreateCustomerCommand(message.UserId, message.Email, message.FirstName,
            message.LastName));
    }
}
