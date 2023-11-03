namespace EITracker.Models
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string? Address { get; set; }
        public string? Qualification { get; set; }

        public virtual ICollection<BookModel> Books { get; set; }
    }
}
