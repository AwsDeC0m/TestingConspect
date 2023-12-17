using Dapper;
using DarkStore.Api.Data;

namespace DarkStore.Api.Repositories;

public class DarkStoreRepository : IDarkStoreRepository
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public DarkStoreRepository(
        ISqliteDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Models.DarkStore>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<Models.DarkStore>("SELECT * FROM DarkStores");
    }

    public async Task<Models.DarkStore?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM DarkStores WHERE Id = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Models.DarkStore>(query, new { Id = id });
    }

    public async Task<bool> CreateAsync(Models.DarkStore darkStore)
    {
        const string query = "INSERT INTO DarkStores (Id, FullName, Number) VALUES (@Id, @FullName, @Number)";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query,
            new { darkStore.Id, darkStore.FullName, darkStore.Number });
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        const string query = "DELETE FROM DarkStores WHERE Id = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }
}
