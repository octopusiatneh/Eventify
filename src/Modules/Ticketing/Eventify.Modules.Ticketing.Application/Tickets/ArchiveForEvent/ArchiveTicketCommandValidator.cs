using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Tickets.ArchiveForEvent;

internal sealed class ArchiveTicketCommandValidator : AbstractValidator<ArchiveTicketCommand>
{
    public ArchiveTicketCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
