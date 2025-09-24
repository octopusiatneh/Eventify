using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.Domain.TicketTypes;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateBatch;

internal sealed class CreateBatchTicketHandler(
    IOrderRepository orderRepository,
    ITicketRepository ticketRepository,
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBatchTicketCommand>
{
    public async Task<Result> Handle(CreateBatchTicketCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        var markTicketsAsIssuedResult = order.IssueTickets();
        if (markTicketsAsIssuedResult.IsFailure)
        {
            return Result.Failure(markTicketsAsIssuedResult.Error);
        }

        List<Ticket> tickets = [];
        foreach (var orderItem in order.OrderItems)
        {
            var ticketType = await ticketTypeRepository.GetAsync(orderItem.TicketTypeId, cancellationToken);
            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(orderItem.TicketTypeId));
            }

            for (int i = 0; i < orderItem.Quantity; i++)
            {
                var ticket = Ticket.Create(order, ticketType);
                tickets.Add(ticket);
            }
        }

        ticketRepository.InsertRange(tickets);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
