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
    public class RoomController : Controller
    {
        private readonly IRoomService _roomData;
        private readonly ICategoryService _categoryData;
        private readonly IMapper _mapper;

        public RoomController(IRoomService rs, ICategoryService cs)
        {
            _roomData = rs;
            _categoryData = cs;
            _mapper = new Mapper(AutomapperConfig.Config);
        }


        
        [HttpGet]
        public ActionResult CreateRoom()
        {

            var categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories());
            var model = new RoomCreateModel(categories);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRoom([FromForm]RoomCreateModel data)
        {
            try
            {
                var res = "Модель пуста!";

                if (ModelState.IsValid)
                {
                    var exists = await _roomData.AllRooms();

                    if (exists.FirstOrDefault(d => d.RoomNumber == data.RoomNumber) == null)
                    {
                        var id = _categoryData.AllCategories().FirstOrDefault(d => d.CategoryId.ToString().Equals(data.CategoryID)).CategoryId;
                        var r = new RoomViewModel() { RoomNumber = data.RoomNumber, CategoryFK = id };

                        await _roomData.AddRoom(_mapper.Map<RoomViewModel, RoomDTO>(r));

                        return RedirectToAction("InitialPage", "Visitor");
                    }

                    res = "Комната с таким номером уже существует.";
                }

                ModelState.AddModelError("", res);

                var categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories());
                data = new RoomCreateModel(categories);

                return View(data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditRoom(string key) 
        {
            try 
            {
                
            var rooms = await _roomData.AllRooms();
            var room = rooms.FirstOrDefault(d=>d.RoomId == Guid.Parse(key));
            var categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_categoryData.AllCategories());
            var selected = _mapper.Map<CategoryDTO, CategoryViewModel>(room.RoomCategory);
            var model = new RoomCreateModel(categories, selected) 
            {
                RoomId = room.RoomId.ToString(),
                RoomNumber = room.RoomNumber,
                CurrentCategory = room.RoomCategory.CategoryName
            };

            return View(model);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRoom([FromForm] RoomCreateModel data) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rooms = await _roomData.AllRooms();
                    var r = rooms.FirstOrDefault(d=>d.RoomId == Guid.Parse(data.RoomId));
                    r.RoomNumber = data.RoomNumber;
                    r.CategoryFK = Guid.Parse(data.CategoryID);
                    await _roomData.EditRoom(r);

                    return RedirectToAction("InitialPage", "Visitor");
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
        public async Task<ActionResult> DeleteRoom(string keyId) 
        {
            try
            {

                await _roomData.DeleteRoom(keyId);

                return RedirectToAction("InitialPage", "Visitor");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
