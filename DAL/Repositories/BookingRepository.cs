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
    public class BookingRepository : IDataRepository<Booking>
    {
        private readonly HotelAppContext db;

        public BookingRepository(HotelAppContext _db)
        {
            db = _db;
        }
        public void Add(Booking data)
        {
            data.BookingId = Guid.NewGuid();
            db.Bookings.Add(data);
        }

        public void Delete(Booking data)
        {
            db.Bookings.Remove(data);
        }

        public IEnumerable<Booking> GetData()
        {
            return db.Bookings.Include("RoomBooking").Include("BookingVisitor");
        }

        public void Update(Booking data)
        {
            try
            {
                var ch = db.Bookings.Find(data.BookingId);
                if (ch != null)
                {
                    db.Entry(ch).State = EntityState.Detached;

                    ch.RoomFK = data.RoomFK;
                    ch.RoomBooking = db.Rooms.FirstOrDefault(d => d.RoomId == data.RoomFK);
                    ch.MoveIn = data.MoveIn;
                    ch.MoveOut = data.MoveOut;
                    ch.CheckedIn = data.CheckedIn;

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
