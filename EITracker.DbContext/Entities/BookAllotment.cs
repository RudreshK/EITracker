namespace EITracker.DbContext.Entities
{
    public class BookAllotment
    {
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AllotmentId { get; set; }
        public DateTime AllotmentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? BookReturnedOn { get; set; }
    }
}
