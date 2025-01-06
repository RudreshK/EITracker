using EITracker.DbContext;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Services;
using EITracker.DbContext.Entities;

namespace EITracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ITypeMapper _typeMapper;
        public LeaveRequestController(LibraryDbContext libraryDbContext, ITypeMapper typeMapper)
        {
            _libraryDbContext = libraryDbContext;
            _typeMapper = typeMapper;
        }

        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetLeaveList(ODataQueryOptions<Models.LeaveRequestModel> options, CancellationToken token)
        {
            IQueryable<LeaveRequest> query = _libraryDbContext.LeaveRequests;
            IQueryable<Models.LeaveRequestModel> projection = _typeMapper.Map(query, options);
            return Ok(projection);
        }

        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> CreateLeaveService([FromBody] Models.LeaveRequestModel model, CancellationToken token)
        {
            LeaveRequest holiday = _typeMapper.Map<Models.LeaveRequestModel, LeaveRequest>(model);
            _libraryDbContext.LeaveRequests.Add(holiday);
            await _libraryDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            model.LeaveId = holiday.LeaveId;
            return Ok(model);
        }
    }
}
