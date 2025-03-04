using Eventify.Modules.Users.Application.Abstractions;
using Eventify.Modules.Users.Application.Abstractions.Identity;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Users.Register;

public sealed class RegisterUserHandler(IIdentityProviderService identityProviderService,
                                        IUserRepository userRepository,
                                        IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password, firstName, lastName) = request;
        var identityRegistrationResult = await identityProviderService.RegisterIdentityUserAsync(
            new IdentityUserModel(email, password, firstName, lastName),
            cancellationToken
        );

        return await identityRegistrationResult.Match(
            onSuccess: RegisterUserAsync(email, firstName, lastName, cancellationToken),
            onFailure: Result.Failure<Guid>
        );
    }

    private Func<string, Task<Result<Guid>>> RegisterUserAsync(string email, string firstName, string lastName, CancellationToken cancellationToken)
    {
        return async (identityId) =>
        {
            var user = User.Create(email, firstName, lastName, identityId);

            await userRepository.InsertAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(user.Id);
        };
    }
}
