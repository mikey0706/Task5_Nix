using AutoMapper;
using Microsoft.Extensions.Configuration;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;
using Task5_Nix.Utils;
using Task5_Nix.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Task5_Nix.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userData;
        private readonly IRegistrationService _tokenService;
        private readonly IMapper _mapper;

        public HomeController(IConfiguration config, IRegistrationService tokenService, IUserService us)
        {
            _config = config;
            _userData = us;
            _tokenService = tokenService;
            _mapper = new Mapper(AutomapperConfig.Config);
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser([FromForm]UserRegistrationModel data)
        {
            try 
            { 
            if (ModelState.IsValid && data.Password.Equals(data.RepeatPassword))
            {
                if (data != null)
                {
                    data.UserId = Guid.NewGuid().ToString();
                    var v = new VisitorViewModel()
                    {
                        Id = data.UserId,
                        VisitorName = data.UserName,
                        Passport = $"{data.PassportSeries}-{data.PassportNum}"
                    };

                    await _userData.AddUser(_mapper.Map<VisitorViewModel, VisitorDTO>(v), data.Password);

                    _tokenService.GenerateJSONWebToken(_config["Jwt:Key"], _config["Jwt:Issuer"], data);

                    return RedirectToAction("InitialPage", "Visitor");


                }

                ModelState.AddModelError("", "Имя пользователя или пароль введены не верно!");

            }

            return View(data);

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] UserLoginModel data)
        {
            try 
            { 
            if (ModelState.IsValid && data!=null)
            { 
                var user =  await _userData.VerifyUser(data.UserName, data.Password);

                    if (user != null)
                    {
                        if (user.isAdmin) data.Role = "admin";

                        data.UserId = user.Id;

                        await Task.Run(()=>_tokenService.GenerateJSONWebToken(_config["Jwt:Key"], _config["Jwt:Issuer"], data));

                        return RedirectToAction("InitialPage", "Visitor");
                    }

                    ModelState.AddModelError("", "Имя пользователя или пароль введены не верно!");

                }

            return View(data);

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public async Task<IActionResult> Logout()
        {
            try 
            {

                await Task.Run(()=>_tokenService.RemoveCookie());

                return RedirectToAction("InitialPage", "Visitor");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


    }
}
