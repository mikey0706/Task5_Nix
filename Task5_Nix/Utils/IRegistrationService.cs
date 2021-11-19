using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.ViewModels;

namespace Task5_Nix.Utils
{
    public interface IRegistrationService
    {
        public void GenerateJSONWebToken(string key, string issuer, UserLoginModel user);
        public Task DeleteCookies();
    }
}
