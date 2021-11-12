using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class RoomDTO
    {
        public Guid RoomId { get; set; }

        public int RoomNumber { get; set; }

        public Guid CategoryFK { get; set; }

        public virtual CategoryDTO RoomCategory { get; set; }

        public virtual ICollection<BookingDTO> RoomBooking { get; set; }
    }
}
