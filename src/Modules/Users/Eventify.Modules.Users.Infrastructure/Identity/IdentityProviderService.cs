using System.Net;
using Eventify.Modules.Users.Application.Abstractions.Identity;
using Eventify.Shared.Domain;
using Microsoft.Extensions.Logging;

namespace Eventify.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    public async Task<Result<string>> RegisterUserAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            userModel.Email,
            userModel.Email,
            userModel.FirstName,
            userModel.LastName,
            EmailVerified: true,
            Enabled: true,
            Credentials: [new CredentialRepresentation(PasswordCredentialType, userModel.Password, Temporary: false)]);

        try
        {
            return await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(ex, "Error registering user {Email}", userModel.Email);

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
