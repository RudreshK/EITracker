using EITracker.DbContext;
using EITracker.DbContext.Entities;
using EITracker.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Services;

namespace ODataDemo.Controllers
{
    [ODataRoutePrefix("Customers")]
    public class CustomersController : ODataController
    {
        private readonly LibraryDbContext _databaseContext;
        private readonly ITypeMapper _mapper;
        public CustomersController(LibraryDbContext databaseContext, ITypeMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 10)]
        public IActionResult GetAuthorsService(ODataQueryOptions<CustomerModel> options, CancellationToken token)
        {
            IQueryable<Customer> query = _databaseContext.Customers;
            IQueryable<CustomerModel> projection = _mapper.Map(query, options);
            return Ok(projection);
        }
    }
}
