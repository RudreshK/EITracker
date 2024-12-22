using EITracker.DbContext;
using Entities = EITracker.DbContext.Entities;
using Models = EITracker.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Services;

namespace ODataDemo.Controllers
{
    [ODataRoutePrefix("Calendar")]
    public class CalendarController : ODataController
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ITypeMapper _typeMapper;
        public CalendarController(LibraryDbContext libraryDbContext, ITypeMapper typeMapper)
        {
            _libraryDbContext = libraryDbContext;
            _typeMapper = typeMapper;
        }

        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 10)]
        public async Task<IActionResult> GetHolidayList(ODataQueryOptions<Models.HolidayList> options, CancellationToken token)
        {
            IQueryable<Entities.HolidayList> query = _libraryDbContext.HolidayLists;
            IQueryable<Models.HolidayList> projection = _typeMapper.Map(query, options);
            return Ok(projection);
        }

        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> CreateHolidayService([FromBody] Models.HolidayList model, CancellationToken token)
        {
            Entities.HolidayList holiday = _typeMapper.Map<Models.HolidayList, Entities.HolidayList>(model);
            _libraryDbContext.HolidayLists.Add(holiday);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.HolidayId = holiday.HolidayId;
            return Ok(model);
        }
    }
}
