using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.Create;

internal sealed class CreateEventHandler(
    IEventRepository eventRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var (categoryId, title, description, location, startsAtUtc, endsAtUtc) = request;

        var category = await categoryRepository.GetAsync(categoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(categoryId));
        }

        var @event = Event.Create(category, title, description, location, startsAtUtc, endsAtUtc);
        await eventRepository.InsertAsync(@event, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return @event.Id;
    }
}
