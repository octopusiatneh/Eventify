using System.Data.Common;
using Dapper;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Users.Get;

internal sealed class GetUserByIdHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(UserResponse.Id)},
                 email AS {nameof(UserResponse.Email)},
                 first_name AS {nameof(UserResponse.FirstName)},
                 last_name AS {nameof(UserResponse.LastName)}
             FROM users.users
             WHERE id = @UserId
             """;

        UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return user;
    }
}
