namespace EITracker.DbContext.Entities
{
    public class Book
    {
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Publication { get; set; }
        public short PublicationYear { get; set; }
        public string ISBN { get; set; }

        public virtual Author Author { get; internal set; }
    }
}
