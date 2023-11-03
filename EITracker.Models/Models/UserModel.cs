// <copyright file="UserModel.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

namespace EITracker.Models
{
    public class UserModel
    {             
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string? Password { get; set; }
        public bool? SendEmail { get; set; }

    }
}
