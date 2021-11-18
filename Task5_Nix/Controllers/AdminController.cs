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

        [HttpGet]
        public async Task<ActionResult> AdminMainPage() 
        {
            var data = await _roomData.UserRooms("Admin");
            
            var rooms = data.Select(d => new RoomInfo()
            {
                RoomNumber = d.RoomNumber,
                RoomCategory = _categoryData.AllCategories()
                .FirstOrDefault(c => c.CategoryId == d.CategoryFK).CategoryName,
                Price = _categoryData.AllCategories()
                .FirstOrDefault(c => c.CategoryId == d.CategoryFK).CategoryDate
                .LastOrDefault().Price

            });

            var categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories());

            var model = new AdminPageViewModel() 
            {
                AdminRooms = rooms,
                MonthProfit = _bookingData.GetProfit(),
                Categories = categories
            };

            return View(model);
        }
    }
}
