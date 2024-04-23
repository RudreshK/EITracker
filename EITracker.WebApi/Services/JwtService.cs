// <copyright file="UserService.cs" company="EcoInsight">
// Copyright (c) EcoInsight. All rights reserved.
// </copyright>
// --------------------------------------

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ODataDemo.Services
{
    public class JwtService
    {
        private readonly SymmetricSecurityKey _key;

        public JwtService(string key)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public string GenerateToken(string username)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("K&*()BIVbocuashOUHIYGGBUOGYI)(*&");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, username)
              }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
