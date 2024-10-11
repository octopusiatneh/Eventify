using FluentValidation;
using MediatR;

namespace Eventify.Modules.Events.Infrastructure.CQRS;

public sealed class Validator<T> : AbstractValidator<T> where T : IRequest
{
}
