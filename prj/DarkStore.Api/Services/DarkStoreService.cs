using System.Diagnostics;
using DarkStore.Api.Logging;
using DarkStore.Api.Repositories;

namespace DarkStore.Api.Services;

public class DarkStoreService : IDarkStoreService
{
    private readonly IDarkStoreRepository _darkstoreRepository;
    private readonly ILoggerAdapter<DarkStoreService> _logger;

    public DarkStoreService(IDarkStoreRepository darkstoreRepository, ILoggerAdapter<DarkStoreService> logger)
    {
        _logger = logger;
        _darkstoreRepository = darkstoreRepository;
    }

    public async Task<IEnumerable<Models.DarkStore>> GetAllAsync()
    {
        _logger.LogInformation("Получение всех магазинов");
        var stopWatch = Stopwatch.StartNew();
        try
        {
            return await _darkstoreRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при получении всех магазинов.");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("Запрос отработал успешно. Длительность: {0}мс", stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<Models.DarkStore?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Получение информации о магазине (GUID): {0}", id);
        var stopWatch = Stopwatch.StartNew();
        try
        {
            return await _darkstoreRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при получении информации о магазине (GUID) {0}", id);
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("Запрос отработал успешно. Магазин (GUID): {0}; Длительность: {1}мс", id, stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> CreateAsync(Models.DarkStore darkStore)
    {
        _logger.LogInformation("Создание магазина (GUID): {0}; наименование: {1}", darkStore.Id, darkStore.FullName);

        var stopWatch = Stopwatch.StartNew();
        try
        {
            return await _darkstoreRepository.CreateAsync(darkStore);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при создании магазина.");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("Магазин с id {0} создан за {1}ms", darkStore.Id, stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        _logger.LogInformation("Удаление магазина с id: {0}", id);
        var stopWatch = Stopwatch.StartNew();
        try
        {
            return await _darkstoreRepository.DeleteByIdAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при удалении магазина с id {0}", id);
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("Магазин с id {0} удален за {1}ms", id, stopWatch.ElapsedMilliseconds);
        }
    }
}
