using FluentValidation;

namespace Eventify.Modules.Events.Application.TicketTypes.CreateTicketType;

internal sealed class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
{
    public  CreateTicketTypeCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().Length(min: 2, max: 5);
        RuleFor(c => c.Currency).NotEmpty().Length(3).WithMessage("Currency should be specified and must be in shorthand format.");
        RuleFor(c => c.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(1).WithMessage("Quantity should be greater than or equal to 1.")
            .LessThanOrEqualTo(200).WithMessage("Quantity should be less than or equal to 200.");
        RuleFor(c => c.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(1).WithMessage("Price should be greater than or equal to 1.")
            .LessThanOrEqualTo(1_000_000).WithMessage("Price should be less than or equal to 1,000,000.");
    }
}
