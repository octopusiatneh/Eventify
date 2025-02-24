using Eventify.Shared.Application.Authorization;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Users.GetPermissions;

internal sealed class GetUserPermissionsHandler(IPermissionService permissionService) : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    public Task<Result<PermissionsResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        => permissionService.GetUserPermissionAsync(request.IdentityId);
}
