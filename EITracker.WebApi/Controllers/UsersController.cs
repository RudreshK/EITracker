
using EITracker.DbContext;
using EITracker.DbContext.Dbo;
using EITracker.Models;
using EITracker.WebApi.Controllers;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataDemo.Services;
using System.Data;
using System.Text;
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
        [HttpGet]
        [ODataRoute("({userId})/roles")]
        [EnableQuery(MaxExpansionDepth = 6, PageSize = 25)]
        public async Task<IActionResult> GetAllRoles(Guid userId, CancellationToken token)
        {
            var roles = await applicationDbContext.Roles.Select(x=> x.Name).ToListAsync();

            if(userId != Guid.Empty)
            {
                roles.Clear();
                var userRoles = await this.applicationDbContext.UserRoles.Where(ur => ur.UserId == userId).Select(c => c.RoleId).ToListAsync();
                foreach (var roleId in userRoles)
                {
                    var role = await this.applicationDbContext.Roles.Where(r => r.Id == roleId).Select(x=>x.Name).FirstOrDefaultAsync();
                    roles.Add(role);
                }
            }
            
            return new JsonResult(
               roles,
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
            UserModel = new UserModel
            {
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
        public async Task<IActionResult> PatchUserService(Guid userId, [FromBody] Delta<UserModel> delta, CancellationToken token)
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
                UserId =await this.GetUniqueEmployeeNumber(token),
                FirstName = companyUser.FirstName,
                LastName = companyUser.LastName,
                UserName = companyUser.Email,
                NormalizedUserName = companyUser.Email.ToUpper(),
                Email = companyUser.Email,
                IsApproved = true,
                EmailConfirmed = true,
                PhoneNumber =companyUser.PhoneNumber,
                DOB = companyUser.DOB.DateTime,
                DOJ = companyUser.DOJ.DateTime,
                IsActive = true,
                CreatedTime = DateTime.UtcNow,
                ModifiedTime =DateTime.UtcNow
            };
            try
            {
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
            }
            catch (Exception ex)
            {
                return BadRequest();

            }
                     
            await applicationDbContext.SaveChangesAsync(token).ConfigureAwait(false);
            return Ok(user);
        }

        /// <summary>
        /// Get unique Pick Ticket Number
        /// </summary>
        /// <param name="eRPPO"></param>
        /// <returns></returns>
        private async Task<string> GetUniqueEmployeeNumber(CancellationToken token)
        {
            string emp = "EI";
            var user = await (this.applicationDbContext.Users.Where(t => t.UserId.StartsWith($"{emp}-"))
                .OrderByDescending(x => x.CreatedTime).FirstOrDefaultAsync(token));
            const int maxUniqueNumber = 99;
            int uniqueNumber = 1;
            if (user == null)
            {
                return $"{emp}-{uniqueNumber:000}";
            }
            else
            {
                int index = user.UserId.Split('-').Length;
                if (user.UserId.Split('-').Length > 1)
                {
                   _ = int.TryParse(user.UserId.Split('-')[index - 1].PadLeft(index, '0')[..3], out uniqueNumber);
                   
                }
                StringBuilder numberToReturn = new($"{emp}-{uniqueNumber:000}");

                numberToReturn.Clear();
                if (uniqueNumber == maxUniqueNumber)
                {
                    uniqueNumber = 1;
                    numberToReturn.Append($"{emp}-{uniqueNumber:000}");
                }
                else
                {
                    ++uniqueNumber;
                    numberToReturn.Append($"{emp}-{uniqueNumber:000}");
                }
                return numberToReturn.ToString();
            }
        }

    }
}
