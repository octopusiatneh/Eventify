using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Carts.AddItem;

internal sealed class AddItemCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.TicketTypeId).NotEmpty();
        RuleFor(X => X.Quantity).NotEmpty().InclusiveBetween(1, 99);
    }
}
