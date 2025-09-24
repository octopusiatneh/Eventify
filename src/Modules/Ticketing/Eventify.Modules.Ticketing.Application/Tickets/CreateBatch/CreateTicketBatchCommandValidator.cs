using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateBatch;

internal sealed class CreateBatchTicketCommandValidator : AbstractValidator<CreateBatchTicketCommand>
{
    public CreateBatchTicketCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}
