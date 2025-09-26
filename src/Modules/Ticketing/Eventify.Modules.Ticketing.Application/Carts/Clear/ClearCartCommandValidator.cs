using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Carts.Clear;

internal sealed class ClearCartCommandValidator : AbstractValidator<ClearCartCommand>
{
    public ClearCartCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

