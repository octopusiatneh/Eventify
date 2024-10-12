
namespace Eventify.Modules.Events.Domain.Events;

public interface IEventRepository
{
    Task InsertAsync(Event @event, CancellationToken cancellationToken = default);
}
