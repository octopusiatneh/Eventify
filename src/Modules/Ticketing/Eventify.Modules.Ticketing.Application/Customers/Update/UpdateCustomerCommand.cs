using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Customers.Update;

public sealed record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
