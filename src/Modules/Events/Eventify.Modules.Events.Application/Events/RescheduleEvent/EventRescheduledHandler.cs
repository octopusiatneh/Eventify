using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class EventRescheduledHandler : IDomainEventHandler<EventRescheduled>
{
    public Task Handle(EventRescheduled notification, CancellationToken cancellationToken) => Task.CompletedTask;
}
