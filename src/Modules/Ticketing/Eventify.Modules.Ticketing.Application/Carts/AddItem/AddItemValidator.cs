using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Carts.AddItem;

internal sealed class AddItemValidator : AbstractValidator<AddItemCommand>
{
    public AddItemValidator()
    {
    }
}
