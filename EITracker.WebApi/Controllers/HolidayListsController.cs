using EITracker.DbContext;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Services;
using EITracker.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace EITracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayListsController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ITypeMapper _typeMapper;
        public HolidayListsController(LibraryDbContext libraryDbContext, ITypeMapper typeMapper)
        {
            _libraryDbContext = libraryDbContext;
            _typeMapper = typeMapper;
        }

        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetHolidayList(ODataQueryOptions<Models.HolidayListModel> options, CancellationToken token)
        {
            IQueryable<HolidayList> query = _libraryDbContext.HolidayLists;
            IQueryable<Models.HolidayListModel> projection = _typeMapper.Map(query, options);
            return Ok(projection);
        }

        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> CreateHolidayService([FromBody] Models.HolidayListModel model, CancellationToken token)
        {
            HolidayList holiday = _typeMapper.Map<Models.HolidayListModel, HolidayList>(model);
            _libraryDbContext.HolidayLists.Add(holiday);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.HolidayId = holiday.HolidayId;
            return Ok(model);
        }

        [HttpDelete("{holidayId}")]
        public async Task<IActionResult> DeleteHoliday([FromODataUri] Guid holidayId, CancellationToken token)
        {          
            HolidayList? entity = await _libraryDbContext.HolidayLists
                .FirstOrDefaultAsync(x => x.HolidayId == holidayId, token);

            _libraryDbContext.HolidayLists.Remove(entity);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);

            return Ok();
        }
    }
}
