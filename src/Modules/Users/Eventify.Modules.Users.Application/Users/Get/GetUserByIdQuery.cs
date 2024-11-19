using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;
using FluentValidation;

namespace Eventify.Modules.Users.Application.Users.Get;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<User>;

internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.Id, cancellationToken);

        return user;
    }
}

internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
