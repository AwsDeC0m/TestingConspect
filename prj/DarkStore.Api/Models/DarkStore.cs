namespace DarkStore.Api.Models;

public class DarkStore
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string FullName { get; set; } = default;

    public int Number { get; set; } = default;
}

public class DarkStoreLogs
{
    public int Id { get; init; } = 0;

    public string Info { get; set; } = default!;

}
