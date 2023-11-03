namespace EITracker.DbContext.Entities
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Qualification { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
