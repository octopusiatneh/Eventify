using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(UserModel userModel, CancellationToken cancellationToken = default);
}
