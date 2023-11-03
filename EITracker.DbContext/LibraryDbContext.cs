using EITracker.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace EITracker.DbContext
{
    public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookAllotment> BookAllotments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(author =>
            {
                author.HasKey(t => t.AuthorId);
                author.Property(t => t.AuthorId).ValueGeneratedOnAdd().IsRequired();

                author.HasMany(t => t.Books).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
            });

            modelBuilder.Entity<Book>(book =>
            {
                book.HasKey(t => new { t.AuthorId, t.BookId });
                book.Property(t => t.BookId).ValueGeneratedOnAdd().IsRequired();
            });

            modelBuilder.Entity<Customer>(customer =>
            {
                customer.HasKey(t => t.CustomerId);
                customer.Property(t => t.CustomerId).ValueGeneratedOnAdd().IsRequired();
            });

            modelBuilder.Entity<BookAllotment>(allotment =>
            {
                allotment.HasKey(t => new { t.AuthorId, t.BookId, t.CustomerId, t.AllotmentId });
                allotment.Property(t => t.AllotmentId).ValueGeneratedOnAdd().IsRequired();
            });

            modelBuilder.Entity<Product>(product =>
            {
                product.ToTable("Product", "dbo");
                product.HasKey(t => new { t.Id});
                product.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
                product.Property(t => t.ProductName).IsRequired();
            });

            modelBuilder.Entity<ChatMessage>(chatMessage =>
            {
                chatMessage.ToTable("ChatMessage", "dbo");
                chatMessage.HasKey(t => new { t.ChatId });
                chatMessage.Property(t => t.ChatId).ValueGeneratedOnAdd().IsRequired();
                chatMessage.Property(t => t.CreatedDate).ValueGeneratedOnAdd();              
            });
        }
    }
}
