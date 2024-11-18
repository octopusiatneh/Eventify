using Eventify.Shared.Application.EventBus;

namespace Eventify.Modules.Users.Contracts.IntegrationMessages;

public sealed class UserRegisteredMessage(
    Guid notificationId,
    DateTime occurredOnUtc,
    Guid userId,
    string email,
    string firstName,
    string lastName) : IntegrationMessage(notificationId, occurredOnUtc)
{
    public Guid UserId { get; private set; } = userId;
    public string Email { get; private set; } = email;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
}
