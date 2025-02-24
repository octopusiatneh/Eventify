using FluentValidation;

namespace Eventify.Modules.Users.Application.Users.Get;

internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
