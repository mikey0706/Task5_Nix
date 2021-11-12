using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }

        public string VisitorFK { get; set; }

        public virtual VisitorDTO BookingVisitor { get; set; }

        public Guid RoomFK { get; set; }

        public virtual RoomDTO RoomBooking { get; set; }

        public DateTime MoveIn { get; set; }

        public DateTime MoveOut { get; set; }

        public bool CheckedIn { get; set; }
    }
}
