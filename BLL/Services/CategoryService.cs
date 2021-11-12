using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _db;
        private IMapper mapper;

        public CategoryService(IUnitOfWork db)
        {
            _db = db;
            mapper = new Mapper(AutomapperConfig.Config);
        }

        public IEnumerable<CategoryDTO> AllCategories()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories.GetData());
        }

        public async Task AddCategory(CategoryDTO data, DateTime? endDate, int price)
        {
            try 
            { 
                var c = mapper.Map<CategoryDTO, Category>(data);
                _db.Categories.Add(c);
                await _db.Save();
                await CatDateCreator(c, endDate, price);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private async Task CatDateCreator(Category c, DateTime? endDate, int price) 
        {
            try
            {
                var cd = new CategoryDate() 
                {
                    CategoryFK = c.CategoryId, 
                    Price = price, 
                    StartDate = DateTime.Now 
                };

                if (endDate.HasValue) cd.EndDate = endDate.Value.Date;

                _db.CategoryDates.Add(cd);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task EditCategory(CategoryDTO data, DateTime startDate, DateTime endDate, int price)
        {
            try 
            { 
                
                var c = mapper.Map<CategoryDTO, Category>(data);
                _db.Categories.Update(c);
                await _db.Save();
                var id = c.CategoryId;
                await EditCatDate(id, startDate, endDate, price);


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async Task EditCatDate(Guid id, DateTime start, DateTime finish, int price) 
        {
            var cd = _db.Categories.GetData().FirstOrDefault(d=>d.CategoryId == id).CategoryDate.ToList()[^1].CatDateId;
            var data = _db.CategoryDates.GetData().FirstOrDefault(d=>d.CatDateId == cd);

            data.StartDate = start.Date;
            data.EndDate = finish.Date;
            data.Price = price;

            _db.CategoryDates.Update(data);
            await _db.Save();
            
        
        }

        public async Task DeleteCategory(CategoryDTO data)
        {
            try 
            { 

                var c = _db.Categories.GetData().FirstOrDefault(d=>d.CategoryId==data.CategoryId);

                _db.Categories.Delete(c);
                await _db.Save();
     
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task<CategoryDTO> FindCategory(string roomId) 
        {
            var r = await Task.Run(()=>
            _db.Rooms.GetData().FirstOrDefault(d=>d.RoomId==Guid.Parse(roomId)));

            var c = await Task.Run(()=>
            _db.Categories.GetData().FirstOrDefault(d=>d.CategoryId == r.CategoryFK));

            return mapper.Map<Category, CategoryDTO>(c);
        }
    }
}
