using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository(EventsDbContext dbContext) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
        => await dbContext.Events.AddAsync(@event, cancellationToken);
}
