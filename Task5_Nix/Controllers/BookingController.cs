using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class BookingController : Controller
    {
        private readonly IUserService _userData;
        private readonly IRoomService _roomData;
        private readonly ICategoryService _categoryData;
        private readonly IBookingService _bookingData;

        public BookingController(IUserService us, IRoomService rs, ICategoryService cs, IBookingService bs)
        {
            _userData = us;
            _roomData = rs;
            _categoryData = cs;
            _bookingData = bs;
        }

        [HttpGet]
        public IActionResult BookingList() 
        {
            var data = _bookingData.AllBookings().Where(d=>d.BookingVisitor!=null);
            var vi = _userData.AllVisitors();

            var model = data.Select(d=> new BookInfo() 
            {
                BookingId = d.BookingId.ToString(),
                VisitorName = d.BookingVisitor.VisitorName,
                RoomNumber = d.RoomBooking.RoomNumber,
                MoveIn = d.MoveIn,
                MoveOut = d.MoveOut,
                CheckedIn = d.CheckedIn
            });

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> BookRoom(string roomId) 
        {
            try 
            {
               
                    var c = await _categoryData.FindCategory(roomId);
                    var r = await _roomData.AllRooms();

                    var model = new BookRoomVM()
                    {
                        RoomFK = Guid.Parse(roomId),
                        RoomNumber = r.FirstOrDefault(d => d.RoomId == Guid.Parse(roomId)).RoomNumber,
                        Price = c.CategoryDate.LastOrDefault().Price
                    };

                    return View(model);
              

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        [HttpPost]
        public async Task<ActionResult> BookRoom([FromForm]BookRoomVM data) 
        {
            try 
            {
            if (User.Identity.IsAuthenticated)
            {
                var ci = (ClaimsIdentity)HttpContext.User.Identity;
                var k = ci.FindFirst(ClaimTypes.NameIdentifier);
                
                var bk = new BookingDTO()
                {
                    VisitorFK = k.Value,
                    RoomFK = data.RoomFK,
                    MoveIn = data.MoveIn,
                    MoveOut = data.MoveOut
                };

                    await _bookingData.AddBooking(bk);

                return RedirectToAction("InitialPage", "Visitor");
             }

                return RedirectToAction("RegisterUser", "Home");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditBooking(string bookId) 
        {
            var bk = _bookingData.AllBookings().FirstOrDefault(d=>d.BookingId == Guid.Parse(bookId));

            var r = await _roomData.AllRooms();
            if (bk != null)
            {
              
                    var model = new BookRoomVM()
                    {
                        BookingId = bookId,
                        RoomFK = bk.RoomFK,
                        VisitorFk = bk.VisitorFK,
                        RoomNumber = r.FirstOrDefault(d => d.RoomId == bk.RoomFK).RoomNumber,
                        MoveIn = bk.MoveIn,
                        MoveOut = bk.MoveOut,
                        CheckedIn = bk.CheckedIn
                    };
                    return View(model);

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditBooking([FromForm] BookRoomVM data) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var r = await _roomData.AllRooms();

                    var bk = new BookingDTO() 
                    {
                        BookingId = Guid.Parse(data.BookingId),
                        RoomFK = r.FirstOrDefault(d=>d.RoomNumber == data.RoomNumber).RoomId,
                        VisitorFK = data.VisitorFk,
                        MoveIn = data.MoveIn,
                        MoveOut = data.MoveOut,
                        CheckedIn = data.CheckedIn
                    };

                    await _bookingData.EditBooking(bk);

                    return RedirectToAction("AdminMainPage", "Admin");
                }
                ModelState.AddModelError("", "Не все поля заполнены!");

                return View();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBooking(string delId) 
        {
            try 
            {
                await _bookingData.DeleteBooking(delId);
                return RedirectToAction("AdminMainPage", "Admin");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}
