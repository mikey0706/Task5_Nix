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

        public CategoryDateDTO FindCategory(Guid id) 
        {
            var res = _db.CategoryDates.GetData().LastOrDefault(c => c.CategoryFK == id);
            return mapper.Map<CategoryDate, CategoryDateDTO>(res);
        }
    }
}
