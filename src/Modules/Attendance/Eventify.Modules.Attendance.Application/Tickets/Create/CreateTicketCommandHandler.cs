using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Application.Tickets.Create;

internal sealed class CreateTicketCommandHandler(
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTicketCommand>
{
    public async Task<Result> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var (ticketId, customerId, eventId, code) = request;

        var ticket = Ticket.Create(ticketId, customerId, eventId, code);
        await ticketRepository.InsertAsync(ticket, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
