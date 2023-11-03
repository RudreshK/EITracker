﻿using EITracker.DbContext.Dbo;

namespace EITracker.Models
{
    public class ChatMessageModel
    {
        public Guid ChatId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
    }
}
