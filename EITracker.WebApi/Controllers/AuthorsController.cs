using EITracker.DbContext;
using EITracker.DbContext.Entities;
using EITracker.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataDemo.Services;

namespace ODataDemo.Controllers
{
    [ODataRoutePrefix("Authors")]
    public class AuthorsController : ODataController
    {
        private readonly LibraryDbContext _databaseContext;
        private readonly ITypeMapper _mapper;
        public AuthorsController(LibraryDbContext databaseContext, ITypeMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 10)]
        public IActionResult GetAuthorsService(ODataQueryOptions<AuthorModel> options, CancellationToken token)
        {
            IQueryable<Author> query = _databaseContext.Authors;
            IQueryable<AuthorModel> projection = _mapper.Map(query, options);
            return Ok(projection);
        }

        [HttpGet]
        [ODataRoute("({authorId})")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetAuthorByIdService(Guid authorId, ODataQueryOptions<AuthorModel> options, CancellationToken token)
        {
            IQueryable<Author> query = _databaseContext.Authors.Where(w => w.AuthorId == authorId);
            IQueryable<AuthorModel> projection = _mapper.Map(query, options);
            return Ok(await projection.SingleOrDefaultAsync(token));
        }

        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> CreateAuthorService([FromBody]AuthorModel model, CancellationToken token)
        {
            Author author = _mapper.Map<AuthorModel, Author>(model);
            _databaseContext.Authors.Add(author);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.Id = author.AuthorId;
            return Ok(model);
        }

        [HttpPatch]
        [ODataRoute("({authorId})")]
        public async Task<IActionResult> PatchAuthorService(Guid authorId, Delta<AuthorModel> delta, CancellationToken token)
        {
            IQueryable<Author> query = _databaseContext.Authors.Where(w => w.AuthorId == authorId);
            Author? author = await query.SingleOrDefaultAsync(token);
            AuthorModel model = _mapper.Map<Author, AuthorModel>(author);
            delta.Patch(model);
            _mapper.Map<AuthorModel, Author>(model, author);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            return Updated(model);
        }

        [HttpDelete]
        [ODataRoute("({authorId})")]
        public async Task<IActionResult> DeleteAuthorService(Guid authorId, CancellationToken token)
        {
            IQueryable<Author> query = _databaseContext.Authors.Where(w => w.AuthorId == authorId);
            Author? author = await query.SingleOrDefaultAsync(token);
            _databaseContext.Authors.Remove(author);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet]
        [ODataRoute("({authorId})/Books")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 10)]
        public IActionResult GetBooksService(Guid authorId, ODataQueryOptions<BookModel> options, CancellationToken token)
        {
            IQueryable<Book> query = _databaseContext.Books.Where(w => w.AuthorId == authorId);
            IQueryable<BookModel> projection = _mapper.Map(query, options);
            return Ok(projection);
        }

        [HttpGet]
        [ODataRoute("({authorId})/Books({bookId})")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetBookByIdService(Guid authorId, Guid bookId, ODataQueryOptions<BookModel> options, CancellationToken token)
        {
            IQueryable<Book> query = _databaseContext.Books.Where(w => w.AuthorId == authorId && w.BookId == bookId);
            IQueryable<BookModel> projection = _mapper.Map(query, options);
            return Ok(await projection.SingleOrDefaultAsync());
        }

        [HttpPost]
        [ODataRoute("({authorId})/Books")]
        public async Task<IActionResult> CreateBookService(Guid authorId, [FromBody] BookModel model, CancellationToken token)
        {
            Book book = _mapper.Map<BookModel, Book>(model);
            Author author = await _databaseContext.Authors.Where(s => s.AuthorId == authorId).SingleAsync(token);
            author.Books.Add(book);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.Id = book.BookId;
            return Ok(model);
        }

        [HttpPatch]
        [ODataRoute("({authorId})/Books({bookId})")]
        public async Task<IActionResult> PatchBookService(Guid authorId, Guid bookId, Delta<BookModel> delta, CancellationToken token)
        {
            IQueryable<Book> query = _databaseContext.Books.Where(w => w.AuthorId == authorId && w.BookId == bookId);
            Book? book = await query.SingleOrDefaultAsync(token);
            BookModel model = _mapper.Map<Book, BookModel>(book);
            delta.Patch(model);
            _mapper.Map<BookModel, Book>(model, book);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            return Updated(model);
        }

        [HttpDelete]
        [ODataRoute("({authorId})/Books({bookId})")]
        public async Task<IActionResult> DeleteBookService(Guid authorId, Guid bookId, CancellationToken token)
        {
            IQueryable<Book> query = _databaseContext.Books.Where(w => w.AuthorId == authorId && w.BookId == bookId);
            Book? book = await query.SingleOrDefaultAsync(token);
            _databaseContext.Books.Remove(book);
            await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
            return NoContent();
        }
    }
}
