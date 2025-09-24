using System.Data.Common;
using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Tickets.ArchiveForEvent;

internal sealed class ArchiveTicketCommandHandler(
    ITicketRepository ticketRepository,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ArchiveTicketCommand>
{
    public async Task<Result> Handle(ArchiveTicketCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var @event = await eventRepository.GetAsync(request.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        var tickets = await ticketRepository.GetForEventAsync(@event, cancellationToken);
        foreach (var ticket in tickets)
        {
            ticket.Archive();
        }

        @event.TicketsArchived();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
