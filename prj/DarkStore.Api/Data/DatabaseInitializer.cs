using Dapper;

namespace DarkStore.Api.Data;

public class DatabaseInitializer
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(ISqliteDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeSqLiteAsync()
    {
        string _info = "first time launch of InitializeSqLiteAsync";

        SqlMapper.AddTypeHandler(new SqLiteGuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));

        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        await connection.ExecuteAsync("CREATE TABLE IF NOT EXISTS " +
                                      " DarkStoreLogs (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      " Info TEXT, DtStamp DATETIME DEFAULT CURRENT_TIMESTAMP); " +
                                      "CREATE TABLE IF NOT EXISTS" +
                                      " DarkStores (Id TEXT PRIMARY KEY,FullName TEXT NOT NULL, Number INTEGER NOT NULL);");
        var _checkInit =
            await connection.QuerySingleOrDefaultAsync<Models.DarkStoreLogs>("SELECT * FROM DarkStoreLogs WHERE Info = @Info",
                new { Info = _info });

        if (_checkInit is null)
        {
            await connection.ExecuteAsync("INSERT INTO DarkStoreLogs (Info) VALUES (@Info)"
                , new { Info = _info });
        }
    }
}
