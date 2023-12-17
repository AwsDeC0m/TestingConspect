namespace DarkStore.Api.Models
{
    public class SimpleDarkStore
    {
        public Guid DarkStoreId = Guid.NewGuid();

        public string DarkStoreFullName = "Super Dark Store ";

        public int DarkStoreNumber = 11;

        public DateOnly DarkStoreDateOfOpen = new(2021, 1, 1);

    }
}
