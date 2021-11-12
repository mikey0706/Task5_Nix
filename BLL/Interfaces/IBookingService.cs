using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        public IEnumerable<BookingDTO> AllBookings();
        public Task AddBooking(BookingDTO data);
        public Task EditBooking(BookingDTO data);
        public Task DeleteBooking(string bkId);
        public int GetProfit();
    }
}
