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
    public class BookingService :IBookingService
    {
        private IUnitOfWork _db;
        private IMapper mapper;

        public BookingService(IUnitOfWork db)
        {
            _db = db;
            mapper = new Mapper(AutomapperConfig.Config);
        }

        public IEnumerable<BookingDTO> AllBookings()
        {
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(_db.Bookings.GetData());
        }

        public async Task AddBooking(BookingDTO data)
        {
            try
            {
                var b = mapper.Map<BookingDTO, Booking>(data);
                b.BookingId = Guid.NewGuid();
                _db.Bookings.Add(b);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task EditBooking(BookingDTO data)
        {
            try 
            { 
                var b = mapper.Map<BookingDTO, Booking>(data);
                _db.Bookings.Update(b);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }

        public async Task DeleteBooking(string bkId)
        {
            try 
            { 
                var b =_db.Bookings.GetData().FirstOrDefault(d=>d.BookingId == Guid.Parse(bkId));
                _db.Bookings.Delete(b);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int GetProfit() 
        {

            var cds = new List<CategoryDate>();
            var bks = _db.Bookings.GetData().Where(d => d.MoveOut < DateTime.Today);

            foreach (var item in bks) 
            {
                var cd = _db.CategoryDates.GetData().FirstOrDefault(d=>d.CategoryFK == item.RoomBooking.CategoryFK);
                cds.Add(cd);
            }

            return cds.Sum(d=>d.Price);

        }
    }
}
