using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryDateRepository : IDataRepository<CategoryDate>
    {
        private readonly HotelAppContext _db;

        public CategoryDateRepository(HotelAppContext db)
        {
            _db = db;
        }

        public void Add(CategoryDate data)
        {
            _db.CategoryDates.Add(data);
        }

        public void Delete(CategoryDate data)
        {
            _db.CategoryDates.Remove(data);
        }

        public IEnumerable<CategoryDate> GetData()
        {
            return _db.CategoryDates.Include("GetCategory");
        }

        public void Update(CategoryDate data)
        {
            try
            {
                var ch = _db.CategoryDates.Local.FirstOrDefault(d => d.CatDateId.Equals(data.CatDateId));
                if (ch != null)
                {
                    _db.Entry(ch).State = EntityState.Detached;

                    ch.CategoryFK = data.CategoryFK;
                    ch.GetCategory = data.GetCategory;
                    ch.StartDate = data.StartDate;
                    ch.EndDate = data.EndDate;
                    ch.Price = data.Price;

                    _db.Entry(ch).State = EntityState.Modified;
                }
            }
            catch
            {
                throw new Exception("Такой объект не существует");
            }
        }
    }
}
