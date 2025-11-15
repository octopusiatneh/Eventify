using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository : IRepositoryBase<TicketType>
{
    Task<bool> ExistsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TicketType>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
}
