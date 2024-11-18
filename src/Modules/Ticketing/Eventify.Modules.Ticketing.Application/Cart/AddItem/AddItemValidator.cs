using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Cart.AddItem;

internal class AddItemValidator : AbstractValidator<AddItemCommand>
{
    public AddItemValidator()
    {
    }
}
