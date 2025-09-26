using Eventify.Modules.Users.Application.Abstractions;
using Eventify.Modules.Users.Application.Abstractions.Identity;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Users.Register;

public sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (email, password, firstName, lastName) = request;
        var identityRegistrationResult = await identityProviderService.RegisterIdentityUserAsync(
            new IdentityUserModel(email, password, firstName, lastName),
            cancellationToken
        );

        return await identityRegistrationResult.Match(
            onSuccess: HandleIdentityRegistrationSuccess(email, firstName, lastName, cancellationToken),
            onFailure: HandleIdentityRegistrationFailure()
        );
    }

    private Func<string, Task<Result<Guid>>> HandleIdentityRegistrationSuccess(string email, string firstName, string lastName, CancellationToken cancellationToken)
    {
        return async (identityId) =>
        {
            var user = User.Create(email, firstName, lastName, identityId);

            await userRepository.InsertAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(user.Id);
        };
    }

    private Func<Error, Task<Result<Guid>>> HandleIdentityRegistrationFailure()
    {
        return async (error) => await Task.FromResult(Result.Failure<Guid>(error));
    }
}
