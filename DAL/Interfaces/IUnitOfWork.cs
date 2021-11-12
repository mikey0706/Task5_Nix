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
        public BookingRepository Bookings { get; }
        public VisitorRepository Visitors { get; }
        public RoomRepository Rooms { get; }
        public CategoryRepository Categories { get; }

        public CategoryDateRepository CategoryDates { get; }

        public Task Save();
    }
}
