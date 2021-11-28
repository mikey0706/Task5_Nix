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
using Task5_Nix.Utils;
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
        private readonly IRegistrationService _registered;
        private readonly IBookingService _bookingService;

        public VisitorController (IUserService us, IRoomService rs, ICategoryService cs, ICategoryDate cd, IRegistrationService reg, IBookingService bs)
        {
            _userData = us;
            _roomData = rs;
            _categoryData = cs;
            _dateCategory = cd;
            _registered = reg;
            _bookingService = bs;
        }

        [HttpGet]
        public async Task<IActionResult> InitialPage() 
        {
            var data=await _roomData.AllRooms();
            var rooms = data.Select(d => new RoomInfo(d, _dateCategory.FindCategory(d.CategoryFK), _categoryData));
            var model = new InitialPageView() { Rooms = rooms };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InitialPage([FromForm] InitialPageView data)
        {
            if (data.CheckIn.Date < DateTime.Now.Date || data.CheckOut.Date < data.CheckIn.Date) 
            {
                ModelState.AddModelError("","Вы ввели некорректную дату.");

                var roomsList = await _roomData.AllRooms();
                var rooms = roomsList.Select(d => new RoomInfo(d, _dateCategory.FindCategory(d.CategoryFK), _categoryData));
                data.Rooms = rooms;

                return View(data);
            }
            var room = await _roomData.RoomsByDate(data.CheckIn, data.CheckOut);

            var model = new InitialPageView();
            model.Rooms = room.Select(d => new RoomInfo(d, _dateCategory.FindCategory(d.CategoryFK), _categoryData));

            return View(model);

        }

        [Authorize]

        [HttpGet]
        public ActionResult VisitorProfile()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var id = _registered.GetCurrentUserId();
                    
                    var user = _userData.AllVisitors().FirstOrDefault(d => d.Id.Equals(id));

                    var data = _bookingService.UserBookings(id);

                    var rooms = data.Select(d => new RoomInfo(d.RoomBooking, _dateCategory.FindCategory(d.RoomBooking.CategoryFK),
                        _categoryData) { BookingId = d.BookingId });

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
                var name = _userData.AllVisitors().FirstOrDefault(d => d.VisitorName.Equals(data.VisitorName));

                if (user!=null & name==null)
                {
                    user.VisitorName = data.VisitorName;
                    user.Passport = $"{data.PassportSeries}-{data.PassportNum}";

                    await _userData.EditUser(user);

                    return RedirectToAction("InitialPage");
                }

                var books= _bookingService.UserBookings(data.Id);

                var rooms = books.Select(d => new RoomInfo(d.RoomBooking, _dateCategory.FindCategory(d.RoomBooking.CategoryFK),
                    _categoryData)
                { BookingId = d.BookingId });

                data.VisitorRooms = rooms;

                ModelState.AddModelError("", "Это имя занято.");

                return View(data);

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
