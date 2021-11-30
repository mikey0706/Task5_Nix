using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IDataRepository<Booking> Bookings { get; }
        public IDataRepository<Visitor> Visitors { get; }
        public IDataRepository<Room> Rooms { get; }
        public IDataRepository<Category> Categories { get; }
        public IDataRepository<CategoryDate> CategoryDates { get; }

        public Task Save();
    }
}
