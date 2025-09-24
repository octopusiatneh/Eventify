using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Domain.Users;

public class UserRegisteredDomainEvent(Guid userId, string email, string firstName, string lastName) : DomainEvent
{
    public Guid UserId { get; init; } = userId;

    public string Email { get; init; } = email;

    public string FirstName { get; init; } = firstName;

    public string LastName { get; init; } = lastName;
}
