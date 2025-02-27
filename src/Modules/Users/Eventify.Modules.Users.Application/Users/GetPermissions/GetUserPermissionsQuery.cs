using Eventify.Shared.Application.Authorization;
using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Users.Application.Users.GetPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
