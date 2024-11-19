using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Cart.AddItem;

internal sealed class AddItemValidator : AbstractValidator<AddItemCommand>
{
    public AddItemValidator()
    {
    }
}
