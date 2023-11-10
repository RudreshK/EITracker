// <copyright file="UserModel.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

namespace EITracker.Models
{
    public class UserModel //: EditableModelFields
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
        public bool IsActive { get; set; }

        public DateTimeOffset DOB { get; set; }
        public DateTimeOffset DOJ { get; set; }
        public DateTimeOffset? DOR { get; set; }

    }
}
