using System.Data.Common;

namespace Eventify.Shared.Application.Database;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
