using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.IntegrationMessages;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.MessageTransport;

namespace Eventify.Modules.Users.Application.Users.Register;

internal sealed class UserRegisteredMessagePublisher(IEventBus eventBus)
    : IDomainEventHandler<UserRegistered>
{
    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var integrationMessage = new UserRegisteredMessage(
            notification.Id,
            notification.OccurredOnUtc,
            notification.Id,
            notification.Email,
            notification.FirstName,
            notification.LastName);

        // Publish message to module that interested
        await eventBus.PublishAsync(integrationMessage, cancellationToken);
    }
}
