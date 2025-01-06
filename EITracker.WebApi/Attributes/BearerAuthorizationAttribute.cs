// -----------------------------------------------------------------------------
// <copyright file="BearerAuthorizationAttribute.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Class for bearer authorization attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BearerAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private IActionResult unauthorizeResult = new JsonResult(new { message = "tokenExpired" }) { StatusCode = StatusCodes.Status401Unauthorized };

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers[Constants.AppSettingValues.RequestHeaderAuthorization].FirstOrDefault();
            if (!string.IsNullOrEmpty(token) && token.Trim().StartsWith(Constants.AppSettingValues.BearerAuthorization, StringComparison.InvariantCultureIgnoreCase))
            {
                token = token.Replace(Constants.AppSettingValues.BearerAuthorization, string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();
                AppConfiguration appConfig = (AppConfiguration)context.HttpContext.RequestServices.GetService(typeof(AppConfiguration));
                var jwtToken = (JwtSecurityToken)JwtTokenActions.ValidateToken(token, appConfig.JwtSecret);
                if (jwtToken != null)
                {
                    context.HttpContext.User = new ClaimsPrincipal(
                                new ClaimsIdentity[]
                                {
                                    new ClaimsIdentity(jwtToken.Claims)
                                });

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { new ClaimsIdentity(jwtToken.Claims) });
                    System.Threading.Thread.CurrentPrincipal = claimsPrincipal;
                }
                else
                {
                    context.Result = this.unauthorizeResult;
                }
            }
            else
            {
                context.Result = this.unauthorizeResult;
            }
        }
    }
}
