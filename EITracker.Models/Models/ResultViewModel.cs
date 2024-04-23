// <copyright file="ResultViewModel.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>
using EITracker.Enums;

namespace EITracker.Model
{

    /// <summary>Result View Model.</summary>
    public class ResultViewModel
    {
        /// <summary>Gets or sets Status.</summary>
        public Status Status { get; set; }

        /// <summary>Gets or sets Message.</summary>
        public string Message { get; set; }

        /// <summary>Gets or sets  Data.</summary>
        public object Data { get; set; }
    }

}