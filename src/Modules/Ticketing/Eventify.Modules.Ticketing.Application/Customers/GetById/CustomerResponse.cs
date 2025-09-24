namespace Eventify.Modules.Ticketing.Application.Customers.GetById;

public sealed record CustomerResponse(Guid Id, string Email, string FirstName, string LastName);
