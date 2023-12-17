using DarkStore.Api.Models;

namespace DarkStore.Api.Services;

public interface IDarkStoreService
{
    Task<IEnumerable<Models.DarkStore>> GetAllAsync();

    Task<Models.DarkStore?> GetByIdAsync(Guid id);

    Task<bool> CreateAsync(Models.DarkStore darkStore);

    Task<bool> DeleteByIdAsync(Guid id);
}
