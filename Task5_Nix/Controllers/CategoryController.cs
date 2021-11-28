using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;
using Task5_Nix.ViewModels;

namespace Task5_Nix.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryData;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService cs) 
        {
            _categoryData = cs;
            _mapper = new Mapper(AutomapperConfig.Config);

        }
        [HttpGet]
        public ActionResult CategoryList()
        {
            var model = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(
                _categoryData.AllCategories());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateCategory() 
        { 
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory([FromForm]CategoryCreateModel data) 
        {
            try
            {

                if (ModelState.IsValid && data.EndDate.Date>DateTime.Now.Date)
                {
                    var c = new CategoryDTO()
                    {
                        CategoryName = data.CategoryName
                    };
                   
                    await _categoryData.AddCategory(c, data.EndDate, data.Price);

                    return RedirectToAction("AdminMainPage", "Admin");
                }
                ModelState.AddModelError("", $"Конечная дата должна быть позже чем: {DateTime.Now.Date}");

                return View(data);
            } 
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCategory(string keyId) 
        {
            try 
            {
                var v = _categoryData.AllCategories().FirstOrDefault(d => d.CategoryId==Guid.Parse(keyId));
                await _categoryData.DeleteCategory(v);

                return RedirectToAction("AdminMainPage", "Admin");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpGet]
        public IActionResult EditCategory(string key) 
        {
            try 
            { 
            var data = _categoryData.AllCategories().FirstOrDefault(d=>d.CategoryId==Guid.Parse(key));
            var model = new CategoryCreateModel() 
            {
            Id = data.CategoryId.ToString(),
            CategoryName = data.CategoryName,
            StartDate = data.CategoryDate.LastOrDefault().StartDate,
            EndDate = data.CategoryDate.LastOrDefault().EndDate,
            Price = data.CategoryDate.LastOrDefault().Price
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
        public async Task<IActionResult> EditCategory([FromForm]CategoryCreateModel data) 
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var c = new CategoryDTO()
                    {
                        CategoryId = Guid.Parse(data.Id),
                        CategoryName = data.CategoryName
                    };

                    await _categoryData.EditCategory(c, data.StartDate.Date, data.EndDate.Date, data.Price);

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
    }
}
