using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private User()
    {
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public List<Role> Roles { get; private set; } = [];

    public static User Create(string email, string firstName, string lastName, string identityId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = identityId,
            Roles = [Role.Member],
        };

        user.Raise(new UserRegistered(user.Id, user.Email, user.FirstName, user.LastName));

        return user;
    }
}
