using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.IntegrationEvents;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.EventBus;

namespace Eventify.Modules.Users.Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            notification.Id,
            notification.Email,
            notification.FirstName,
            notification.LastName
        );

        // Publish message to module that interested
        await eventBus.PublishAsync(userRegisteredIntegrationEvent, cancellationToken);
    }
}
