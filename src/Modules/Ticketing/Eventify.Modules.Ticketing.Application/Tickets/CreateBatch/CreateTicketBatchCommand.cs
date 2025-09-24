using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateBatch;

public sealed record CreateBatchTicketCommand(Guid OrderId) : ICommand;
