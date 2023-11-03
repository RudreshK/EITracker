using EITracker.DbContext.Dbo;
using EITracker.Models;

namespace EITracker.UI.Services
{
    public interface IChatManager
    {
        Task<List<ApplicationUser>> GetUsersAsync();
        Task SaveMessageAsync(ChatMessageModel message);
        Task<List<ChatMessageModel>> GetConversationAsync(Guid contactId);
        Task<ApplicationUser> GetUserDetailsAsync(Guid userId);
    }
}
