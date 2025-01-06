// -----------------------------------------------------------------------------
// <copyright file="RefreshTokenProvider.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Services
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using EITracker.DbContext.Dbo;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Class for refresh token generators.
    /// </summary>
    public class RefreshTokenProvider : IUserTwoFactorTokenProvider<ApplicationUser>
    {
        private readonly AppConfiguration configuration;

        public RefreshTokenProvider(AppConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        private bool ValidateToken(string refreshToken)
        {
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(refreshToken));
            RefreshTokenModel refreshTokenObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RefreshTokenModel>(decodedToken);
            return refreshTokenObject.ValidUpto >= DateTime.UtcNow.Ticks;
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var refreshTokenObject = new RefreshTokenModel { Id = user.Id, ValidUpto = DateTime.UtcNow.AddDays(this.configuration.RefreshTokenLifetimeInDays).Ticks };
            var refreshTokenCache = Newtonsoft.Json.JsonConvert.SerializeObject(refreshTokenObject);

            byte[] bytes = Encoding.GetEncoding(0).GetBytes(refreshTokenCache);
            string refreshToken = Convert.ToBase64String(bytes);

            await manager.RemoveAuthenticationTokenAsync(user, Constants.AppSettingValues.RefreshTokenProvider, purpose);
            await manager.SetAuthenticationTokenAsync(user, Constants.AppSettingValues.RefreshTokenProvider, purpose, refreshToken);

            return refreshToken;
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            string verify = await manager.GetAuthenticationTokenAsync(user, Constants.AppSettingValues.RefreshTokenProvider, purpose);
            return verify == token && this.ValidateToken(token);
        }
    }
}
