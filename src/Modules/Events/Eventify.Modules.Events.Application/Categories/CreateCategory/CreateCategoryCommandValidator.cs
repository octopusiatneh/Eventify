﻿using FluentValidation;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
