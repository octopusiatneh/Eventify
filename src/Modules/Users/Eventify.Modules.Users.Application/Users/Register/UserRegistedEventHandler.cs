using Eventify.Modules.Users.Application.Users.Get;
using Eventify.Modules.Users.Contracts.IntegrationMessages;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.EventBus;
using MediatR;

namespace Eventify.Modules.Users.Application.Users.Register;

internal sealed class UserRegisteredHandler(ISender sender, IEventBus eventBus)
    : IDomainEventHandler<UserRegistered>
{
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        // Fetch for the registered user then construct the integration message
        var result = await sender.Send(new GetUserByIdQuery(notification.UserId), cancellationToken);
        var user = result.Value;
        var integrationMessage = new UserRegisteredMessage(
            notification.Id, notification.OccurredOnUtc,
            user.Id, user.Email, user.FirstName, user.LastName);

        // Publish message to module that interested
        await eventBus.PublishAsync(integrationMessage, cancellationToken);
    }
}
