using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Infrastructure.Database;

namespace Eventify.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository : IEventRepository
{
    private readonly EventsDbContext _dbContext;

    public EventRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
    {
        await _dbContext.Events.AddAsync(@event, cancellationToken);
    }
}
