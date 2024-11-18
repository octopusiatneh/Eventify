using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Customers.Create;

public sealed record CreateCustomerCommand(Guid Id, string Email, string FirstName, string LastName) : ICommand;
