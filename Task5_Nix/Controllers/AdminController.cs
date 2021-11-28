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
        private readonly ICategoryDate _dateCategory;
        private readonly IMapper _mapper;

        public AdminController(IRoomService rs, ICategoryService cs, IBookingService bs, ICategoryDate cd)
        {
            _roomData = rs;
            _categoryData = cs;
            _bookingData = bs;
            _dateCategory = cd;
            _mapper = new Mapper(AutomapperConfig.Config);
        }

        public async Task<ActionResult> AdminMainPage() 
        {
            var rooms = await _roomData.UserRooms("Admin");

            var data = rooms.Select(d => new RoomInfo(d, _dateCategory.FindCategory(d.CategoryFK), _categoryData));

            var model = new AdminPage()
            {
                AdminRooms = data,
                MonthProfit = _bookingData.GetProfit(),
                Categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories())
        };
            return View(model);
        }
    }
}
