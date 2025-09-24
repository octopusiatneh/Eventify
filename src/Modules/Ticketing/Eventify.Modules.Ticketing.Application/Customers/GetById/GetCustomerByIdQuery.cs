using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Customers.GetById;

public sealed record GetCustomerByIdQuery(Guid CustomerId) : IQuery<CustomerResponse>;
