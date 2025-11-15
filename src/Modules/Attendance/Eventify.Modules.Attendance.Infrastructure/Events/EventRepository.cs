using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Attendance.Infrastructure.Events;

internal sealed class EventRepository(AttendanceDbContext dbContext) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task InsertAsync(Event @event, CancellationToken cancellationToken = default)
        => await dbContext.Events.AddAsync(@event, cancellationToken);
}
