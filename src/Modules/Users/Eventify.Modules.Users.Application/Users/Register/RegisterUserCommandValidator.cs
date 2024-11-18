using FluentValidation;

namespace Eventify.Modules.Users.Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.Lastname).NotEmpty();
    }
}
