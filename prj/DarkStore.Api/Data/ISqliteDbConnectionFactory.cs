using System.Data;

namespace DarkStore.Api.Data;

public interface ISqliteDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}
