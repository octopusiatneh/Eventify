using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeRepository : ITicketTypeRepository
{
    private readonly EventsDbContext _dbContext;

    public TicketTypeRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.TicketTypes.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task InsertAsync(TicketType ticketType, CancellationToken cancellationToken = default)
        => await _dbContext.TicketTypes.AddAsync(ticketType, cancellationToken);
}
