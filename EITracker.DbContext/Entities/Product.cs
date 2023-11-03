namespace EITracker.DbContext.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }
    }
}
