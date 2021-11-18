using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Task5_Nix.Controllers;
using Task5_Nix.Utils;
using Task5_Nix.ViewModels;
using Xunit;

namespace AppControllerTests
{
    public class HomeControllerTest
    {

        private readonly HomeController controller;


        public HomeControllerTest(IUserService us) 
        {
            var _config = new Mock<IConfiguration>();
            var _tokenService = new Mock<IRegistrationService>();

            controller = new HomeController(_config.Object, _tokenService.Object, us);

        }



        [Fact]
        public void RedirectRegisterTest()
        {
            var user = new UserRegistrationModel() 
            {
                UserName = "Boba", 
                PassportSeries="tt", 
                PassportNum="34234234234", 
                Password = "1234bobaaa", 
                RepeatPassword = "1234bobaaa" 
            };

            var action =  Task.Run(()=>controller.RegisterUser(data: user)).Result;
            
            var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);

            Assert.Equal("InitialPage",res.ActionName);
        }

        [Fact]
        public void ViewResultRegisterTest()
        {
            var user = new UserRegistrationModel() 
            { 
                UserName = "Boba", 
                PassportSeries = "ttttt", 
                PassportNum = "34234234234", 
                Password = "1234bi", 
                RepeatPassword = "1234bibo" 
            };

            var action = Task.Run(() => controller.RegisterUser(data: user)).Result;

            var res = Assert.IsAssignableFrom<ViewResult>(action);

            Assert.Same(user, res.Model);

        }

        [Fact]
        public void LoginPositiveTest()
        {
            var user = new UserLoginModel() { UserName = "Admin", Password = "12345admin" };

            var action = controller.Login(data: user).Result;

            var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);

            Assert.Equal("InitialPage", res.ActionName);
        }

        [Fact]
        public void LoginNegativeTest()
        {
            var user = new UserLoginModel() { UserName = "AdminE", Password = "1345ad" };

            var action = controller.Login(data: user).Result;

            var res = Assert.IsAssignableFrom<ViewResult>(action);

            Assert.Same(user, res.Model);
        }

        [Fact]
        public void LogoutTest() 
        {
            var action = Task.Run(()=>controller.Logout()).Result;

            var res = Assert.IsAssignableFrom<RedirectToActionResult>(action);
            Assert.Equal("InitialPage", res.ActionName);
        }

    }
}
