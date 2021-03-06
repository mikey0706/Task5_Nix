using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class RegistrationService : IRegistrationService
    {
        private const int tokenTimeout = 5;
        private readonly IHttpContextAccessor _accessor;
        private readonly SignInManager<IdentityUser> _manager;


        public RegistrationService(IHttpContextAccessor accessor, SignInManager<IdentityUser> manager) 
        {
            _accessor = accessor;
            _manager = manager;
        }
        public void GenerateJSONWebToken(string key, string issuer, UserLoginModel user)
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
            _accessor.HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
        }

        public string GetCurrentUserId() 
        {
            var ci = (ClaimsIdentity)_accessor.HttpContext.User.Identity;
            return ci.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task DeleteCookies() 
        {
            await _manager.SignOutAsync();

            foreach (var cookie in _accessor.HttpContext.Request.Cookies.Keys)
            {
                _accessor.HttpContext.Response.Cookies.Delete(cookie);
            }
        }
    }
}
