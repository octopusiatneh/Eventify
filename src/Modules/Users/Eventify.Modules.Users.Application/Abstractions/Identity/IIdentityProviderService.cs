using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterIdentityUserAsync(IdentityUserModel userModel, CancellationToken cancellationToken = default);
}
