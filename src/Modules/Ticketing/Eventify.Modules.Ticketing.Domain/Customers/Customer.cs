using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer()
    {
    }

    public Customer(Guid id, string email, string firstName, string lastName)
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

    public static Customer Create(Guid id, string email, string firstName, string lastName)
        => new(id, email, firstName, lastName);

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
