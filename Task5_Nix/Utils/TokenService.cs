using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task5_Nix.ViewModels;

namespace Task5_Nix.Utils
{
    public class TokenService : ITokenService
    {
        private const int tokenTimeout = 5;
        public string GenerateJSONWebToken(string key, string issuer, UserLoginModel user)
        {


            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name, user.UserName ),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.UserId)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer,
              issuer,
              claims,
              expires: DateTime.Now.AddMinutes(tokenTimeout),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
