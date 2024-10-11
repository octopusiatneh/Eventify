using System.Data.Common;

namespace Eventify.Modules.Events.Application.Events;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
