using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private User()
    {
    }

    public User(Guid id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public static User Create(string email, string firstName, string lastname)
        => new(Guid.NewGuid(), email, firstName, lastname);
}
