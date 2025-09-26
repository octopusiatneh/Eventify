using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Carts.Get;

internal sealed class GetCartQueryValidator : AbstractValidator<GetCartQuery>
{
    public GetCartQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

