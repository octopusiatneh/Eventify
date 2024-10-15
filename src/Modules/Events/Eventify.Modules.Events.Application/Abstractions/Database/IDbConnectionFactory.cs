using System.Data.Common;

namespace Eventify.Modules.Events.Application.Abstractions.Database;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
