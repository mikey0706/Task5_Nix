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
    public class RoomRepository : IDataRepository<Room>
    {
        private readonly HotelAppContext db;

        public RoomRepository(HotelAppContext _db)
        {
            db = _db;
        }
        public void Add(Room data)
        {
            db.Rooms.Add(data);
        }

        public void Delete(Room data)
        {
            db.Rooms.Remove(data);
        }

        public IEnumerable<Room> GetData()
        {
            return db.Rooms.Include("RoomBooking").Include("RoomCategory");
        }

        public void Update(Room data)
        {
            try
            {
                var ch = db.Rooms.Find(data.RoomId);
                if (ch != null)
                {
                    db.Entry(ch).State = EntityState.Detached;

                    ch.RoomBooking = data.RoomBooking;
                    ch.RoomCategory = data.RoomCategory;
                    ch.CategoryFK = data.CategoryFK;
                    ch.RoomCategory = data.RoomCategory;
                    ch.RoomNumber = data.RoomNumber;

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
