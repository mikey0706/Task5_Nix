using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;
using Task5_Nix.ViewModels;

namespace Task5_Nix.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IRoomService _roomData;
        private readonly ICategoryService _categoryData;
        private readonly IBookingService _bookingData;
        private readonly IMapper _mapper;

        public AdminController(IRoomService rs, ICategoryService cs, IBookingService bs)
        {
            _roomData = rs;
            _categoryData = cs;
            _bookingData = bs;
            _mapper = new Mapper(AutomapperConfig.Config);
        }

        public async Task<ActionResult> AdminMainPage() 
        {
            var rooms = await _roomData.UserRooms("Admin");
            
            var model = rooms.Select(d => new RoomInfo()
            {
                RoomNumber = d.RoomNumber,
                RoomCategory = _categoryData.AllCategories()
                .FirstOrDefault(c => c.CategoryId == d.CategoryFK).CategoryName,
                Price = _categoryData.AllCategories()
                .FirstOrDefault(c => c.CategoryId == d.CategoryFK).CategoryDate
                .LastOrDefault().Price

            });

            var mod = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories());

            ViewData["Profit"] = _bookingData.GetProfit();
            TempData["Categories"] = mod;

            return View(model);
        }
    }
}
