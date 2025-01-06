// -----------------------------------------------------------------------------
// <copyright file="FsbJwtSecurityTokenHandler.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EITracker.Services
{
    public class FsbJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        public FsbJwtSecurityTokenHandler()
        {
            this.OutboundClaimTypeMap.Remove(ClaimTypes.NameIdentifier);
            this.OutboundClaimTypeMap.Remove(ClaimTypes.Name);
            this.OutboundClaimTypeMap.Remove(ClaimTypes.Email);
            this.OutboundClaimTypeMap.Remove(ClaimTypes.GivenName);
            this.OutboundClaimTypeMap.Remove(ClaimTypes.Surname);
            this.OutboundClaimTypeMap.Remove(ClaimTypes.Role);
        }
    }
}
