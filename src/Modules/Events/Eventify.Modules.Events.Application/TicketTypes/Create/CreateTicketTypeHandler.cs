using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.TicketTypes.Create;

public class CreateTicketTypeHandler(IUnitOfWork unitOfWork, IEventRepository eventRepository, ITicketTypeRepository ticketTypeRepository)
    : ICommandHandler<CreateTicketTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        var (eventId, name, price, currency, quantity) = request;
        var @event = await eventRepository.GetAsync(eventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure<Guid>(EventErrors.NotFound(eventId));
        }

        var ticketType = TicketType.Create(@event, name, currency, price, quantity);
        await ticketTypeRepository.InsertAsync(ticketType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);


        return ticketType.Id;
    }
}
