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

            if (signInResult != null && await _userManager.CheckPasswordAsync(signInResult, model.Password))
            {
                result.Status = Status.Success;
                result.Message = $"Welcome {model.UserName}";
                result.Data = "Success";

                var token = _jwtService.GenerateToken(model.UserName);
                var refreshToken = string.Empty;

                return Ok(new
                {
                    data = result,
                    token,
                    refreshToken
                });
            }
            else
            {
                result.Status = signInResult != null ? Status.Error : Status.Fail;
                result.Message = signInResult != null ? "Invalid password" : "Invalid login";
                result.Data = signInResult != null ? $"<li>Invalid password attempt - {model.UserName}</li>" : "";

                return Unauthorized(new { data = result });
            }
        }

    }
}