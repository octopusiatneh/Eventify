using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.Reschedule;

internal sealed class RescheduleEventCommandHandler : ICommandHandler<RescheduleEventCommand, Guid>
{
    public Task<Result<Guid>> Handle(RescheduleEventCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
