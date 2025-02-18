using Eventify.Modules.Users.Application.Abstractions.Data;
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

        var userModel = new UserModel(email, password, firstName, lastName);
        var result = await identityProviderService.RegisterUserAsync(userModel, cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var identityId = result.Value;
        var user = User.Create(email, firstName, lastName, identityId);
        await userRepository.InsertAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
