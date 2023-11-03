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
    [ODataRoutePrefix("products")]
    public class ProductsController : ODataController
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ITypeMapper _typeMapper;

        public ProductsController(LibraryDbContext libraryDbContext, ITypeMapper typeMapper)
        {
            _libraryDbContext = libraryDbContext;
            _typeMapper = typeMapper;
        }
        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 10)]
        public async Task<IActionResult> GetProducts(ODataQueryOptions<ProductModel> options, CancellationToken token)
        {
            IQueryable<Product> query = _libraryDbContext.Products;
            IQueryable<ProductModel> projection = _typeMapper.Map(query, options);
            return Ok(projection);
        }
        [HttpGet]
        [ODataRoute("({productId})")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetProdutByIdService(Guid productId, ODataQueryOptions<ProductModel> options, CancellationToken token)
        {
            IQueryable<Product> query = _libraryDbContext.Products.Where(w => w.Id == productId);
            IQueryable<ProductModel> projection = _typeMapper.Map(query, options);
            return Ok(await projection.SingleOrDefaultAsync(token));
        }
        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> CreateProductService([FromBody] ProductModel model, CancellationToken token)
        {
            Product product = _typeMapper.Map<ProductModel, Product>(model);
            _libraryDbContext.Products.Add(product);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.Id = product.Id;
            return Ok(model);
        }

        [HttpPatch]
        [ODataRoute("({productId})")]
        public async Task<IActionResult> PatchProductService(Guid productId, Delta<ProductModel> delta, CancellationToken token)
        {
            IQueryable<Product> query = _libraryDbContext.Products.Where(w => w.Id == productId);
            Product? product = await query.SingleOrDefaultAsync(token);
            ProductModel model = _typeMapper.Map<Product, ProductModel>(product);
            delta.Patch(model);
            _typeMapper.Map<ProductModel, Product>(model, product);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            return Updated(model);
        }

        [HttpDelete]
        [ODataRoute("({productId})")]
        public async Task<IActionResult> DeleteProductService(Guid productId, CancellationToken token)
        {
            IQueryable<Product> query = _libraryDbContext.Products.Where(w => w.Id == productId);
            Product? product = await query.SingleOrDefaultAsync(token);
            _libraryDbContext.Products.Remove(product);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            return NoContent();
        }

    }
}
