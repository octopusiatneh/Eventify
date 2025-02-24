using Microsoft.AspNetCore.Authorization;

namespace Eventify.Shared.Infrastructure.Authorization;

internal sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; private set; }
}
