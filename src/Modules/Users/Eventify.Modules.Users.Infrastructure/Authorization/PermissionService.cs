using Dapper;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.Authorization;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(IDbConnectionFactory dbConnectionFactory) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionAsync(string identityId)
    {
        await using var dbConnection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
                SELECT DISTINCT
                    u.id as {nameof(UserPermission.UserId)},
                    rp.permission_code as {nameof(UserPermission.Permission)}
                FROM users.users u
                JOIN users.user_roles ur ON ur.user_id = u.id
                JOIN users.role_permissions rp ON rp.role_name = ur.role_name
                WHERE u.identity_id = @IdentityId
            """;

        List<UserPermission> permissions = [.. await dbConnection.QueryAsync<UserPermission>(sql, new { identityId })];
        if (permissions.Any())
        {
            return new PermissionsResponse(
                permissions[0].UserId,
                [.. permissions.Select(p => p.Permission)]
            );
        }

        return UserErrors.NotFound(identityId);
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
