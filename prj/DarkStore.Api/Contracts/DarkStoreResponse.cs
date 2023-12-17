namespace DarkStore.Api.Contracts;

public class DarkStoreResponse
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = default!;

    public int Number { get; init; } = default!;
}
