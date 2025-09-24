using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Ticketing.Infrastructure.Tickets;

internal sealed class TicketRepository(TicketingDbContext dbContext) : ITicketRepository
{
    public async Task<Ticket?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IEnumerable<Ticket>> GetForEventAsync(Event @event, CancellationToken cancellationToken = default)
        => await dbContext.Tickets.Where(t => t.EventId == @event.Id).ToListAsync(cancellationToken);

    public void InsertRange(IEnumerable<Ticket> tickets)
        => dbContext.Tickets.AddRange(tickets);
}

