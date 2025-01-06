// -----------------------------------------------------------------------------
// <copyright file="JwtTokenActions.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using EITracker.DbContext.Dbo;
    using Microsoft.IdentityModel.Tokens;
    
    internal static class JwtTokenActions
    {
        internal static string CreateNewToken(string secret, double sessionTimeoutInMinutes, List<Claim> claims)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new FsbJwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(sessionTimeoutInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        internal static async Task<string> CreateAndCacheNewToken(Guid companyId, ApplicationUser user, string cacheConnectionString, string secret, double sessionTimeoutInMinutes, string[] roles)
        {
            return await CreateAndCacheNewToken(
                user.Id,
                cacheConnectionString,
                secret,
                sessionTimeoutInMinutes,
                roles,
                new Claim(Constants.AppSettingValues.ClaimCompanyId, companyId.ToString()),
                new Claim(Constants.AppSettingValues.ClaimApplicationUserId, user.Id.ToString()),
                new Claim(Constants.AppSettingValues.ClaimApplicationUserFirstName, user.FirstName),
                new Claim(Constants.AppSettingValues.ClaimApplicationUserLastName, user.LastName),
                new Claim(Constants.AppSettingValues.ClaimApplicationUserEmail, user.Email));
        }

        internal static async Task<string> CreateAndCacheNewToken(Guid userId, string cacheConnectionString, string secret, double sessionTimeoutInMinutes, string[] roles, params Claim[] claims)
        {
            var listOfClaims = claims.ToList();
            listOfClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            string token = CreateNewToken(secret, sessionTimeoutInMinutes, listOfClaims);

            //using (CacheStorage cache = new CacheStorage(cacheConnectionString))
            //{
            //    string segment = GetJwtTokenSegmentToCache(token);
            //    await cache.AddValueAsync(segment, userId.ToString(), sessionTimeoutInMinutes);
            //}

            return token;
        }

        internal static SecurityToken ValidateToken(string token, string secret)
        {
            try
            {
                var tokenHandler = new FsbJwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static async Task InvalidateAuthTokens(Guid userId, string redisConnectionString, double timeoutInMinutes, params string[] tokensToBlacklist)
        {
            string segment;
            if (tokensToBlacklist != null && tokensToBlacklist.Any())
            {
                //using (CacheStorage cache = new CacheStorage(redisConnectionString))
                //{
                //    Guid outGuid;
                //    foreach (var tokenToBlacklist in tokensToBlacklist)
                //    {
                //        segment = GetJwtTokenSegmentToCache(tokenToBlacklist);
                //        if (await cache.ContainsKeyAsync(segment)
                //            && Guid.TryParse(await cache.GetValueAsync(segment), out outGuid)
                //            && userId == outGuid)
                //        {
                //            // black listing auth token.
                //            await cache.AddValueAsync(segment, Constants.AppSettingValues.BlacklistedToken, timeoutInMinutes);
                //        }
                //    }
                //}
            }
        }

        internal static string GetJwtTokenSegmentToCache(string token)
        {
            string[] segments = (token ?? throw new InvalidOperationException()).Split('.');
            if (segments.Length > 0)
                return segments[segments.Length - 1];
            else
                throw new InvalidOperationException();
        }
    }
}
