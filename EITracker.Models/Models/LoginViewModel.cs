// <copyright file="LoginViewModel.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace EITracker.Models
{
    /// <summary>Gets or sets.</summary>
    public class LoginViewModel
    {       
        /// <summary>Gets or sets.</summary>
        public string? UserName { get; set; }

        /// <summary>Gets or sets.</summary>
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
