// -----------------------------------------------------------------------------
// <copyright file="RefreshTokenModel.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Services
{
    using System;

    internal class RefreshTokenModel
    {
        public Guid Id { get; set; }
        public long ValidUpto { get; set; }
    }
}
