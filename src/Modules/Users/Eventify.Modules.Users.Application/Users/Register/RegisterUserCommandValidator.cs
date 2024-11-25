using FluentValidation;

namespace Eventify.Modules.Users.Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(3);
    }
}
