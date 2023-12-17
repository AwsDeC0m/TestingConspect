using DarkStore.Api.Models;
using DarkStore.Api.Contracts;

namespace DarkStore.Api.Mappers;

public static class DarkStoreMapper
{
    public static DarkStoreResponse ToDarkStoreResponse(this Models.DarkStore darkStore)
    {
        return new DarkStoreResponse
        {
            Id = darkStore.Id,
            FullName = darkStore.FullName + ", № " + darkStore.Number.ToString(), 
            Number = darkStore.Number
        };
    }
}
