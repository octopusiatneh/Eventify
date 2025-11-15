using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Attendance.Infrastructure.Tickets;

internal sealed class TicketRepository(AttendanceDbContext dbContext) : ITicketRepository
{
    public async Task<Ticket?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task InsertAsync(Ticket ticket, CancellationToken cancellationToken = default)
        => await dbContext.Tickets.AddAsync(ticket, cancellationToken);
}
