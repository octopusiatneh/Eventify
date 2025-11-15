using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeRepository(EventsDbContext dbContext) : ITicketTypeRepository
{
    public async Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.TicketTypes.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default)
        => await dbContext.TicketTypes.AddAsync(ticketType, cancellationToken);

    public async Task<bool> ExistsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
        => await dbContext.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);

    public async Task<IReadOnlyCollection<TicketType>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
        => await dbContext.TicketTypes.Where(t => t.EventId == eventId).ToListAsync(cancellationToken);
}
