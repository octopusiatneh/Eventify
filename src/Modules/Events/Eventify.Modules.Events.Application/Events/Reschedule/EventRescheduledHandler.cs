using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.Reschedule;

internal sealed class EventRescheduledHandler : IDomainEventHandler<EventRescheduledEvent>
{
    public Task Handle(EventRescheduledEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
}
