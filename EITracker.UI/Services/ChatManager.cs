using EITracker.DbContext.Dbo;
using EITracker.Models;

namespace EITracker.UI.Services
{
    public class ChatManager : IChatManager
    {
        private readonly HttpClient _httpClient;

        public ChatManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatMessageModel>> GetConversationAsync(Guid contactId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessageModel>>($"api/chat/{contactId}");
        }
        public async Task<ApplicationUser> GetUserDetailsAsync(Guid userId)
        {
            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/chat/users/{userId}");
        }
        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<ApplicationUser>>("api/chat/users");
            return data;
        }
        public async Task SaveMessageAsync(ChatMessageModel message)
        {
            await _httpClient.PostAsJsonAsync("api/chat", message);
        }
    }
}
