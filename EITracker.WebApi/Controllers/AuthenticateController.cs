// <copyright file="AuthenticateController.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>

using EITracker.DbContext.Dbo;
using EITracker.Enums;
using EITracker.Model;
using EITracker.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Services;

namespace ODataDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateController(JwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            this._userManager = userManager;
        }


        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] LoginViewModel model)
        {
            var result = new ResultViewModel();

            var signInResult = await _userManager.FindByEmailAsync(model.UserName);

            result.Status = signInResult != null ? Status.Success : Status.Error;
            result.Message = signInResult != null
                ? $"Welcome {model.UserName}"
                : "Invalid login";
            result.Data = signInResult != null
                ? $"Success"
            : $"<li>Invalid login attempt - {signInResult}</li>";

            if (signInResult != null && await _userManager.CheckPasswordAsync(signInResult, model.Password))
            {
                var token = this._jwtService.GenerateToken(model.UserName);
                var refreshToken = string.Empty;
                return this.Ok(new
                {
                    data = result,
                    token,
                    refreshToken,
                });
            }
            else
            {
                return this.Unauthorized(new { data = result });
            }
        }

    }
}