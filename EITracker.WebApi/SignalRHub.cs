﻿using EITracker.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChat.Server
{
    public class SignalRHub : Hub
    {
        public async Task SendMessageAsync(ChatMessageModel message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }
    }
}
