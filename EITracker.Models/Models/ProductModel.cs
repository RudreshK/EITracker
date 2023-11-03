namespace EITracker.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }
    }
}
