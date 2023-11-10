// -----------------------------------------------------------------------------
// <copyright file="EditableModelFields.cs" company="ecoInsight, Inc.">
// Copyright © ecoInsight Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Models
{
    public class EditableModelFields
    {
        /// <summary>
        /// Created by Id
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Created datetime
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Modified by Id
        /// </summary>
        public Guid ModifiedById { get; set; }

        /// <summary>
        /// Modified date time
        /// </summary>
        public DateTimeOffset Modified { get; set; }
    }
}
