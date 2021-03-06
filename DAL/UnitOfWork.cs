using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private HotelAppContext db;
        private VisitorRepository _visitors;
        private RoomRepository _rooms;
        private BookingRepository _bookings;
        private CategoryRepository _categories;
        private CategoryDateRepository _catDate;
        private bool isDisposed;

        public UnitOfWork(HotelAppContext _db)
        {
            db = _db;
        }

        public IDataRepository<Booking> Bookings
        {
            get
            {
                if (_bookings == null)
                {
                    _bookings = new BookingRepository(db);
                }

                return _bookings;
            }
        }

        public IDataRepository<Visitor>Visitors
        {
            get
            {
                if (_visitors == null)
                {
                    _visitors = new VisitorRepository(db);
                }

                return _visitors;
            }
        }

        public IDataRepository<Room> Rooms
        {
            get
            {
                if (_rooms == null)
                {
                    _rooms = new RoomRepository(db);
                }

                return _rooms;
            }
        }

        public IDataRepository<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoryRepository(db);
                }

                return _categories;
            }
        }

        public IDataRepository<CategoryDate> CategoryDates
        {
            get
            {
                if (_catDate == null)
                {
                    _catDate = new CategoryDateRepository(db);
                }

                return _catDate;
            }
        }

        public async Task Save()
        {
            await Task.Run(()=>db.SaveChanges());
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                db.Dispose();
                isDisposed = true;
            }
        }
    }
}
