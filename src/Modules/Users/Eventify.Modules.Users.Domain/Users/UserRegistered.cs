using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Domain.Users;

public class UserRegistered(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
