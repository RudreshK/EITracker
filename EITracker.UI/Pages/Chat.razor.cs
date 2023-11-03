using EITracker.DbContext.Dbo;
using EITracker.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace EITracker.UI.Pages
{
    public partial class Chat
    {
        [CascadingParameter] public HubConnection hubConnection { get; set; }
        [Parameter] public string CurrentMessage { get; set; }
        [Parameter] public Guid CurrentUserId { get; set; }
        [Parameter] public string CurrentUserEmail { get; set; }
        private List<ChatMessageModel> messages = new List<ChatMessageModel>();
        private async Task SubmitAsync()
        {
            if (!string.IsNullOrEmpty(CurrentMessage) && !string.IsNullOrEmpty(ContactId))
            {
                //Save Message to DB
                var chatHistory = new ChatMessageModel()
                {
                    Message = CurrentMessage,
                    ToUserId = Guid.Parse(ContactId),
                    CreatedDate = DateTime.Now

                };
                await _chatManager.SaveMessageAsync(chatHistory);
                chatHistory.FromUserId = CurrentUserId;
                await hubConnection.SendAsync("SendMessageAsync", chatHistory, CurrentUserEmail);
                CurrentMessage = string.Empty;
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await _jsRuntime.InvokeAsync<string>("ScrollToBottom", "chatContainer");
        }
        protected override async Task OnInitializedAsync()
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder().WithUrl(_navigationManager.ToAbsoluteUri("/signalRHub")).Build();
            }
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }
            hubConnection.On<ChatMessageModel, string>("ReceiveMessage", async (message, userName) =>
            {
                if ((Guid.Parse(ContactId) == message.ToUserId && CurrentUserId == message.FromUserId) || (Guid.Parse(ContactId) == message.FromUserId && CurrentUserId == message.ToUserId))
                {

                    if ((Guid.Parse(ContactId) == message.ToUserId && CurrentUserId == message.FromUserId))
                    {
                        messages.Add(new ChatMessageModel { Message = message.Message, CreatedDate = message.CreatedDate, FromUser = new ApplicationUser() { Email = CurrentUserEmail } });
                        await hubConnection.SendAsync("ChatNotificationAsync", $"New Message From {userName}", ContactId, CurrentUserId);
                    }
                    else if ((Guid.Parse(ContactId) == message.FromUserId && CurrentUserId == message.ToUserId))
                    {
                        messages.Add(new ChatMessageModel { Message = message.Message, CreatedDate = message.CreatedDate, FromUser = new ApplicationUser() { Email = ContactEmail } });
                    }
                    await _jsRuntime.InvokeAsync<string>("ScrollToBottom", "chatContainer");
                    StateHasChanged();
                }
            });
            await GetUsersAsync();
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            CurrentUserId = Guid.Parse(user.Claims.Where(a => a.Type == "sub").Select(a => a.Value).FirstOrDefault());
            CurrentUserEmail = user.Claims.Where(a => a.Type == "name").Select(a => a.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(ContactId))
            {
                await LoadUserChat(Guid.Parse(ContactId));
            }
        }
        public List<ApplicationUser> ChatUsers = new List<ApplicationUser>();
        [Parameter] public string ContactEmail { get; set; }
        [Parameter] public string ContactId { get; set; }
        async Task LoadUserChat(Guid userId)
        {
            var contact = await _chatManager.GetUserDetailsAsync(userId);
            ContactId = contact.Id.ToString();
            ContactEmail = contact.Email;
            _navigationManager.NavigateTo($"chat/{ContactId}");
            messages = new List<ChatMessageModel>();
            messages = await _chatManager.GetConversationAsync(Guid.Parse(ContactId));
        }
        private async Task GetUsersAsync()
        {
            ChatUsers = await _chatManager.GetUsersAsync();
        }
    }
}
