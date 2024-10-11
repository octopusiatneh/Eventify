using Eventify.Modules.Events.Application.Abstractions.CQRS;
using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Events;

namespace Eventify.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventHandler : ICommandHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var (title, description, location, startsAtUtc, endsAtUtc) = request;

        var @event = Event.Create(title, description, location, startsAtUtc, endsAtUtc, EventStatus.Draft);
        _eventRepository.Insert(@event);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        @event.Raise(new EventCreated(@event.Id));

        return @event.Id;
    }
}
