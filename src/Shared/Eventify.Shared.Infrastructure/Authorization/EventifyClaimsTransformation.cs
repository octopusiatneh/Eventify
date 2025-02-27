using System.Security.Claims;
using Eventify.Shared.Application.Authentication;
using Eventify.Shared.Application.Authorization;
using Eventify.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Infrastructure.Authorization;

internal sealed class EventifyClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(c => c.Type == EventifyClaims.Sub))
        {
            return principal;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var identityId = principal.GetIdentityId();
        var userPermissionsResult = await permissionService.GetUserPermissionAsync(identityId);

        userPermissionsResult.Match(
            onSuccess: (permissionResponse) => Transform(principal, permissionResponse),
            onFailure: (error) => throw new EventifyException(nameof(IPermissionService.GetUserPermissionAsync), error)
        );

        return principal;
    }

    /// <summary>
    /// Transforms the given <see cref="ClaimsPrincipal"/> by adding claims based on the provided <see cref="PermissionsResponse"/>.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/> to transform.</param>
    /// <param name="permissionResponse">The <see cref="PermissionsResponse"/> containing user permissions.</param>
    /// <returns>The transformed <see cref="ClaimsPrincipal"/> with added claims.</returns>
    private static ClaimsPrincipal Transform(ClaimsPrincipal principal, PermissionsResponse permissionResponse)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(EventifyClaims.Sub, permissionResponse.UserId.ToString()));

        foreach (var permission in permissionResponse.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(EventifyClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
