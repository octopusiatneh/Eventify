using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Ticketing.Infrastructure.Events;

internal sealed class EventRepository(TicketingDbContext dbContext) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public void Insert(Event @event)
        => dbContext.Events.Add(@event);
}

