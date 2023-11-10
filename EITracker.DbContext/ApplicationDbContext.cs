// -----------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using EITracker.DbContext.Dbo;
using EITracker.DbContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
            builder.Entity<ApplicationUser>(author =>
            {
                author.HasKey(t => t.Id);
                author.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
                author.Property(t => t.CreatedTime).ValueGeneratedOnAdd().IsRequired();
                author.Property(t => t.ModifiedTime).ValueGeneratedOnAdd().IsRequired();
                author.Property(t => t.ConcurrencyStamp).IsRowVersion();
            });
        }
    }
}