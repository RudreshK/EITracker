namespace EITracker.DbContext.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? Email { get; set; }
    }
}
