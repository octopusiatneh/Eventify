using Eventify.Modules.Events.Application.Abstractions;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.Clock;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.Reschedule;

internal sealed class RescheduleEventCommandHandler(
    IEventRepository eventRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<RescheduleEventCommand>
{
    public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        var (eventId, startsAtUtc, endsAtUtc) = request;
        if (startsAtUtc < dateTimeProvider.NowUtc)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        var @event = await eventRepository.GetAsync(eventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(eventId));
        }

        @event.Reschedule(startsAtUtc, endsAtUtc);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
