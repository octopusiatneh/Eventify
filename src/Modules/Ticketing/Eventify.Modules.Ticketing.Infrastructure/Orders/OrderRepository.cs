using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Ticketing.Infrastructure.Orders;

internal sealed class OrderRepository(TicketingDbContext dbContext) : IOrderRepository
{
    public async Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Orders
            .Include(o => o.OrderItems)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);

    public void Insert(Order order)
        => dbContext.Orders.Add(order);
}

