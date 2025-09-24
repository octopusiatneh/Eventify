using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Events.Create;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(200);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(1000);
        RuleFor(c => c.Location).NotEmpty().MaximumLength(200);
        RuleFor(c => c.StartsAtUtc).NotEmpty().GreaterThan(DateTime.UtcNow);
        RuleFor(c => c.EndsAtUtc)
            .GreaterThan(c => c.StartsAtUtc)
            .When(c => c.EndsAtUtc.HasValue);
    }
}
