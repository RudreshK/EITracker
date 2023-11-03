using EITracker.DbContext;
using EITracker.DbContext.Dbo;
using EITracker.DbContext.Entities;
using EITracker.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataDemo.Services;
using System.Security.Claims;
using Entities = EITracker.DbContext.Entities;

namespace EITracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ODataController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LibraryDbContext _context;
        private readonly ApplicationDbContext _userContext;
        private readonly ITypeMapper _typeMapper;
        public ChatController(UserManager<ApplicationUser> userManager, LibraryDbContext context, ApplicationDbContext userContext, ITypeMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _userContext = userContext;
            _typeMapper = mapper;
        }
        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetConversationAsync(Guid contactId)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var messages = await _context.ChatMessages
                    .Where(h => (h.FromUserId == contactId && h.ToUserId == Guid.Parse(userId)) || (h.FromUserId == Guid.Parse(userId) && h.ToUserId == contactId))
                    .OrderBy(a => a.CreatedDate)
                    .Include(a => a.FromUser)
                    .Include(a => a.ToUser)
                    .Select(x => new ChatMessageModel
                    {
                        FromUserId = x.FromUserId,
                        Message = x.Message,
                        CreatedDate = x.CreatedDate,
                        ChatId = x.ChatId,
                        ToUserId = x.ToUserId,
                        ToUser = x.ToUser,
                        FromUser = x.FromUser
                    }).ToListAsync();
            return Ok(messages);
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            var allUsers = await _userContext.Users.Where(user => user.Id != Guid.Parse(userId)).ToListAsync();
            return Ok(allUsers);
        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserDetailsAsync(string userId)
        {
            var user = await _userContext.Users.Where(user => user.Id == Guid.Parse(userId)).FirstOrDefaultAsync();
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatMessageModel message)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
            message.FromUserId = Guid.Parse(userId);
            message.CreatedDate = DateTime.Now;
            message.ToUser = await _userContext.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
            Entities.ChatMessage chatMessage = _typeMapper.Map<ChatMessageModel, ChatMessage>(message);
            await _context.ChatMessages.AddAsync(chatMessage);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
