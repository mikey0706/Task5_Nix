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
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly SignInManager<IdentityUser> _userSignIn;

        public RegistrationService(IHttpContextAccessor ha, SignInManager<IdentityUser> sm) 
        {
            _userSignIn = sm;
            _httpAccessor = ha;
        }
        public void GenerateJSONWebToken(string key, string issuer, IRegistrationData user)
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

            
            _httpAccessor.HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
            
        }

        public void RemoveCookie()
        {
            Task.Run(()=>_userSignIn.SignOutAsync());
            foreach (var item in _httpAccessor.HttpContext.Request.Cookies.Keys)
            {
                _httpAccessor.HttpContext.Response.Cookies.Delete(item);
            }
        }
    }
}
