using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventHandler : ICommandHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventHandler(IEventRepository eventRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var (categoryId, title, description, location, startsAtUtc, endsAtUtc) = request;

        var category = await _categoryRepository.GetAsync(categoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(categoryId));
        }

        var result = Event.Create(category, title, description, location, startsAtUtc, endsAtUtc);
        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        Event @event = result.Value;
        await _eventRepository.InsertAsync(@event, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        @event.Raise(new EventCreated(@event.Id));

        return @event.Id;
    }
}
