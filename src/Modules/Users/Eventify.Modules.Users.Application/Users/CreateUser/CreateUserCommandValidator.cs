﻿using FluentValidation;

namespace Eventify.Modules.Users.Application.Users.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.Lastname).NotEmpty();
    }
}