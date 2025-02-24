using System.Security.Claims;
using Eventify.Shared.Application.Exceptions;

namespace Eventify.Shared.Application.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        return Guid.TryParse(principal?.FindFirst(EventifyClaims.Sub)?.Value, out var parsedUserId)
            ? parsedUserId
            : throw new EventifyException("userId is missing");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new EventifyException("userIdentity is missing");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        var permissionClaims = principal?.FindAll(EventifyClaims.Permission)
            ?? throw new EventifyException("permissions is missing");

        return permissionClaims
            .Select(x => x.Value)
            .ToHashSet();
    }
}
