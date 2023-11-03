
using EITracker.DbContext;
using EITracker.DbContext.Dbo;
using EITracker.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataDemo.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ODataDemo.Controllers
{
    [ODataRoutePrefix("users")]
    public class UsersController : ODataController
    {

        private readonly ITypeMapper _mapper;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext applicationDbContext;
        public UsersController(ITypeMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            this._mapper = mapper;
            this.Configuration = configuration;
            this._userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 25)]
        public async Task<IActionResult> GetAllUsers(ODataQueryOptions<UserModel> options, CancellationToken token)
        {
            List<UserModel> UserModel = new List<UserModel>();
            var applicationUsers = await this.applicationDbContext.Users.Select(x => new UserModel
            {
                Id = x.Id,
                UserId = x.UserId,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync();
         
            return new JsonResult(
                applicationUsers,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute("({userId})")]
        [EnableQuery(MaxExpansionDepth = 6)]
        public async Task<IActionResult> GetProdutByIdService(Guid userId, ODataQueryOptions<UserModel> options, CancellationToken token)
        {
            UserModel UserModel = new UserModel();
            var apUsers = await this.applicationDbContext.Users.Where(w => w.Id == userId).FirstOrDefaultAsync();
            UserModel = new UserModel {
                Id = apUsers.Id,
                UserId = apUsers.UserId,
                Email = apUsers.Email,
                FirstName = apUsers.FirstName,
                LastName = apUsers.LastName,
                Roles = (List<string>)await _userManager.GetRolesAsync(apUsers),
                PhoneNumber = apUsers.PhoneNumber,
            };
            return Ok(UserModel);
        }

        [HttpPatch]
        [ODataRoute("({userId})")]
        public async Task<IActionResult> PatchUserService(Guid userId,[FromBody] Delta<UserModel> delta, CancellationToken token)
        {
            IQueryable<ApplicationUser> query = applicationDbContext.Users.Where(w => w.Id == userId);
            ApplicationUser? user = await query.SingleOrDefaultAsync();
            UserModel model = _mapper.Map<ApplicationUser, UserModel>(user);
            delta.Patch(model);
            _mapper.Map<UserModel, ApplicationUser>(model, user);
            await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
            return NoContent();
        }
        /// <summary>
        /// Add company user.
        /// </summary>
        /// <param name="ApplicationUser">company.</param>
        /// <returns>Application User.</returns>
        [HttpPost]
        [ODataRoute("")]
        public async Task<IActionResult> PostUser([FromBody] UserModel companyUser, CancellationToken token)
        {
            if (companyUser == null)
            {
                return BadRequest();
            }
            var user = new ApplicationUser
            {
                UserId = companyUser.UserId,
                FirstName = companyUser.FirstName,
                LastName = companyUser.LastName,
                UserName = companyUser.Email,
                NormalizedUserName = companyUser.Email.ToUpper(),
                Email = companyUser.Email,
                IsApproved = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, companyUser.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, companyUser.Roles);
            }
            else
            {
                var errorResponse = new ErrorResponse();
                errorResponse.error = new Error(result.Errors.First().Code, result.Errors.First().Description);
                return this.BadRequest(errorResponse);
            }
            await applicationDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            return Ok(user);
        }
    }
}
