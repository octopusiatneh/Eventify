using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private User()
    {
    }

    private User(Guid id, string email, string firstName, string lastName, string identityId)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IdentityId = identityId;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public static User Create(string email, string firstName, string lastName, string identityId)
    {
        var id = Guid.NewGuid();
        var user = new User(id, email, firstName, lastName, identityId);
        user.Raise(new UserRegistered(id));

        return user;
    }
}
