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
    public class VisitorRepository : IDataRepository<Visitor>
    {
        private readonly HotelAppContext db;

        public VisitorRepository(HotelAppContext _db)
        {
            db = _db;
        }
        public void Add(Visitor data)
        {
            
            db.Visitors.Add(data);
        }

        public void Delete(Visitor data)
        {
            db.Visitors.Remove(data);
        }

        public IEnumerable<Visitor> GetData()
        {
            return db.Visitors.Include("BookingOrders");
        }

        public void Update(Visitor data)
        {
            try
            {
                var ch = db.Visitors.Local.FirstOrDefault(d => d.Id.Equals(data.Id.ToString()));
                if (ch != null)
                {
                    db.Entry(ch).State = EntityState.Detached;

                    ch.VisitorName = data.VisitorName;
                    ch.Passport = data.Passport;

                    db.Entry(ch).State = EntityState.Modified;
                }
            }
            catch
            {
                throw new Exception("Такой объект не существует");
            }
        }
    }
}
