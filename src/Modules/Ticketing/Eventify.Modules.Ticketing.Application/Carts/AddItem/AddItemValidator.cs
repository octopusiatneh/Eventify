using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Carts.AddItem;

internal sealed class AddItemValidator : AbstractValidator<AddItemCommand>
{
    public AddItemValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.TicketTypeId).NotEmpty();
        RuleFor(X => X.Quantity).NotEmpty().InclusiveBetween(1, 10);
    }
}
