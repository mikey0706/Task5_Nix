
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Task5_Nix.Controllers;
using Task5_Nix.Utils;
using Task5_Nix.ViewModels;
using Xunit;

namespace AppControllerTest
{
    public class HomeControllerTests
    {
        private readonly HomeController controller;

        public HomeControllerTests(IUserService us) 
        {
            var token = new Mock<IRegistrationService>().Object;
            var config = new Mock<IConfiguration>().Object;
            controller = new HomeController(config, token, us);
        }

        [Fact]
        public void LoginPositiveTest()
        {
          var user = new UserLoginModel()
            {
                UserName = "Admin",
                Password = "12345admin"
            };
          var action =  Task.Run(()=>controller.Login(user)).Result;
          var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);
          Assert.Equal("InitialPage", res.ActionName);
        }

        [Fact]
        public void LoginNegativeTest()
        {
            var user = new UserLoginModel()
            {
                UserName = "AdminE",
                Password = "12345ad"
            };
            var action = Task.Run(() => controller.Login(user)).Result;
            var res = Assert.IsAssignableFrom<ViewResult>(action);
            Assert.Same(user, res.Model);
        }

        [Fact]
        public void RegstrationPositiveTest() 
        {
            var user = new UserRegistrationModel()
            {
                UserName = "Anatolii",
                PassportSeries = "aa",
                PassportNum = "23423412",
                Password = "12345anatolii",
                RepeatPassword = "12345anatolii"
            };
            var action = Task.Run(() => controller.RegisterUser(user)).Result;
            var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);
            Assert.Equal("InitialPage", res.ActionName);
        }

        [Fact]
        public void RegstrationNegativeTest()
        {
            var user = new UserRegistrationModel()
            {
                UserName = "Anatolii",
                PassportSeries = "aaaaa",
                PassportNum = "23423412",
                Password = "12345anatolii",
                RepeatPassword = "12345ana"
            };
            var action = Task.Run(() => controller.RegisterUser(user)).Result;
            var res = Assert.IsAssignableFrom<ViewResult>(action);
            Assert.Same(user, res.Model);
        }

        [Fact]
        public void LogoutTest()
        {
            var action = Task.Run(() => controller.Logout()).Result;
            var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);
            Assert.Equal("InitialPage", res.ActionName);
        }
    }
}
