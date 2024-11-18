using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventHandler : ICommandHandler<RescheduleEventCommand, Guid>
{
    public Task<Result<Guid>> Handle(RescheduleEventCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
