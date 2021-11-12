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
    public class CategoryRepository : IDataRepository<Category>
    {
        private readonly HotelAppContext db;
        public CategoryRepository(HotelAppContext _db)
        {
            db = _db;
        }
        public void Add(Category data)
        {
            db.Categories.Add(data);
        }

        public void Delete(Category data)
        {
            db.Categories.Remove(data);
        }

        public IEnumerable<Category> GetData()
        {
            return db.Categories.Include("CategoryDate").Include("CategoryRoom");
        }

        public void Update(Category data)
        {
            try
            {
                var ch = db.Categories.Find(data.CategoryId);
                if (ch != null)
                {
                    db.Entry(ch).State = EntityState.Detached;
                    ch.CategoryName = data.CategoryName;
                    ch.CategoryDate = data.CategoryDate;
                    ch.CategoryRoom = data.CategoryRoom;
                    db.Entry(data).State = EntityState.Modified;
                }
            }
            catch
            {
                throw new Exception("Такой объект не существует");
            }
        }
    }
}
