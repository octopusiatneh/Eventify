using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Ticketing.Infrastructure.Customers;

internal sealed class CustomerRepository(TicketingDbContext dbContext) : ICustomerRepository
{
    public async Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Customers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task InsertAsync(Customer customer, CancellationToken cancellationToken = default)
        => await dbContext.Customers.AddAsync(customer, cancellationToken);
}
