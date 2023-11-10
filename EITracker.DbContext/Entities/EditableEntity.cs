// -----------------------------------------------------------------------------
// <copyright file="EditableEntity.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.DbContext.Entities
{
    public abstract class EditableEntity : EditableEntityCreatedUser
    {
        public Guid ModifiedById { get; set; }
        public DateTime ModifiedTime { get; set; }
        public byte[] ConcurrencyStamp { get; set; }
    }
}
