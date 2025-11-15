using FluentValidation;

namespace Eventify.Modules.Attendance.Application.Tickets.Create;

internal sealed class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(c => c.TicketId).NotEmpty();
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Code).NotEmpty().MaximumLength(30);
    }
}
