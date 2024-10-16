﻿using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.Events;

internal sealed class EventRepository : IEventRepository
{
    private readonly EventsDbContext _dbContext;

    public EventRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
        => await _dbContext.Events.AddAsync(@event, cancellationToken);
}
