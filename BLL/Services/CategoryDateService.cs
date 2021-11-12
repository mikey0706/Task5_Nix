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
    public class CategoryDateService : ICategoryDate
    {
        private IUnitOfWork _db;
        private IMapper mapper;
        public CategoryDateService(IUnitOfWork db)
        {
            _db = db;
            mapper = new Mapper(AutomapperConfig.Config);
        }

        public IEnumerable<CategoryDateDTO> AllCatDate()
        {
            return mapper.Map<IEnumerable<CategoryDate>, IEnumerable<CategoryDateDTO>>(_db.CategoryDates.GetData());
        }

        public async Task AddCatDate(CategoryDateDTO data)
        {
            try 
            { 
            var b = mapper.Map<CategoryDateDTO, CategoryDate>(data);
            _db.CategoryDates.Add(b);
            await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task DeleteCatDate(CategoryDateDTO data)
        {
            try { 

                var b = mapper.Map<CategoryDateDTO, CategoryDate>(data);
                _db.CategoryDates.Delete(b);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task EditCatDate(Guid id, CategoryDateDTO data)
        {
            try
            {

                var user = mapper.Map<CategoryDateDTO, CategoryDate>(data);
                _db.CategoryDates.Update(user);
                await _db.Save();
            }
            catch (Exception ex)
            {
              throw  ex.InnerException;
            }

        }
    }
}
