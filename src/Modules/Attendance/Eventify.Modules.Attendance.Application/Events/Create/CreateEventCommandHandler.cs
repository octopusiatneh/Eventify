using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Application.Events.Create;

internal sealed class CreateEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand>
{
    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var (eventId, title, description, location, startsAtUtc, endsAtUtc) = request;

        var @event = Event.Create(eventId, title, description, location, startsAtUtc, endsAtUtc);
        await eventRepository.InsertAsync(@event, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
