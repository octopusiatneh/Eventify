using Eventify.Shared.Domain;

namespace Eventify.Shared.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionAsync(string identityId);
}
