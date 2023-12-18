// -----------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;

namespace EITracker.DbContext.Dbo
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or set UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or set FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or set LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or set Last Login
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or set IsApproved
        /// </summary>
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? DOR { get; set; }

        public Guid CreatedById { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid ModifiedById { get; set; }
        public DateTime ModifiedTime { get; set; }
        public byte[] ConcurrencyStamp { get; set; }

    }
}
