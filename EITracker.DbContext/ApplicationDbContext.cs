// -----------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using EITracker.DbContext.Dbo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EITracker.DbContext

{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">Database connection information.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);         
        }
    }
}