using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Attendance.Infrastructure.Attendees;

internal sealed class AttendeeRepository(AttendanceDbContext dbContext) : IAttendeeRepository
{
    public async Task<Attendee?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Attendees.SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task InsertAsync(Attendee attendee, CancellationToken cancellationToken = default)
        => await dbContext.Attendees.AddAsync(attendee, cancellationToken);
}
