using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task5_Nix.Models;
using Task5_Nix.ViewModels;

namespace Task5_Nix.Controllers
{
    [AllowAnonymous]
    public class VisitorController : Controller
    {
        private readonly IUserService _userData;
        private readonly IRoomService _roomData;
        private readonly ICategoryService _categoryData;
        private readonly ICategoryDate _dateCategory;
        private readonly IMapper _mapper;

        public VisitorController (IUserService us, IRoomService rs, ICategoryService cs, ICategoryDate cd)
        {
            _userData = us;
            _roomData = rs;
            _categoryData = cs;
            _dateCategory = cd;
            _mapper = new Mapper(AutomapperConfig.Config);
        }

        [HttpGet]
        public async Task<IActionResult> InitialPage() 
        {
             var room =await _roomData.AllRooms();
            var model = room.Select(d => new RoomInfo()
            {
                Id = d.RoomId.ToString(),
                RoomNumber = d.RoomNumber,
                RoomCategory = d.RoomCategory.CategoryName,
                Price = _dateCategory.AllCatDate().LastOrDefault(c => c.CategoryFK == d.CategoryFK).Price

            });
            return View(model);
        }

        [Authorize]

        [HttpGet]
        public async Task<ActionResult> VisitorProfile()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var ci = (ClaimsIdentity)HttpContext.User.Identity;
                    var k = ci.FindFirst(ClaimTypes.NameIdentifier);

                    var user = _userData.AllVisitors().FirstOrDefault(d => d.Id.Equals(k.Value));

                    var data = await _roomData.UserRooms(user.VisitorName);

                    var rooms = data.Select(d => new RoomInfo()
                    {
                        Id = d.RoomId.ToString(),
                        RoomNumber = d.RoomNumber,
                        RoomCategory = _categoryData.AllCategories()
                            .FirstOrDefault(c => c.CategoryId == d.CategoryFK).CategoryName,
                        Price = _dateCategory.AllCatDate().LastOrDefault(c => c.CategoryFK == d.CategoryFK).Price

                    });
                    var model = new VisitorProfile()
                    {
                        Id = user.Id,
                        VisitorName = user.VisitorName,
                        PassportSeries = user.Passport.Substring(0,2),
                        PassportNum = user.Passport.Substring(3),
                        VisitorRooms = rooms
                    };
                    return View(model);
                }
                return RedirectToAction("InitialPage");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VisitorProfile([FromForm] VisitorProfile data) 
        {
            try
            {
                var user = _userData.AllVisitors().FirstOrDefault(d=>d.Id.Equals(data.Id));

                if (user!=null)
                {
                    user.VisitorName = data.VisitorName;
                    user.Passport = $"{data.PassportSeries}-{data.PassportNum}";

                    await _userData.EditUser(user);

                    return RedirectToAction("InitialPage");
                }

                ModelState.AddModelError("", "Такой пользователь не найден");

                return View();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAccount(string userId) 
        {
            try
            {
                var exists = _userData.AllVisitors().FirstOrDefault(d => d.Id.Equals(userId));

                if (exists != null)
                {

                    await _userData.DeletUser(exists);

                    return RedirectToAction("Logout", "Home");
                }

                ModelState.AddModelError("", "Такой пользователь не найден");

                return View();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
