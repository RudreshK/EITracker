namespace EITracker.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Publication { get; set; }
        public short PublicationYear { get; set; }
        public string ISBN { get; set; }

        public virtual AuthorModel Author { get; set; }
    }
}
