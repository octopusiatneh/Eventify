using Eventify.Modules.Events.Application.Abstractions;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.PublishEvent;

internal sealed class PublishEventCommandHandler(
    IEventRepository eventRepository,
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PublishEventCommand>
{
    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        var eventId = request.EventId;

        var @event = await eventRepository.GetAsync(eventId, cancellationToken);
        var hasTicketTypes = await ticketTypeRepository.ExistsByEventIdAsync(eventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(eventId));
        }

        if (!hasTicketTypes)
        {
            return Result.Failure(EventErrors.NoTicketsFound);
        }

        if (@event.Status is not EventStatus.Draft)
        {
            return Result.Failure(EventErrors.NotDraft);
        }

        @event.Publish();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
