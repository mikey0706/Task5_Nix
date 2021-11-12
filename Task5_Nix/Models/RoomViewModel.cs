using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.Models
{
    public class RoomViewModel
    {
        public Guid RoomId { get; set; }

        public int RoomNumber { get; set; }

        public Guid CategoryFK { get; set; }

        public CategoryViewModel CategoryRoom { get; set; }

        public ICollection<BookingViewModel> RoomBooking { get; set; }
    }
}
