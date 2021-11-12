using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.Models
{
    public class BookingViewModel
    {
        public Guid BookingId { get; set; }

        public string VisitorFK { get; set; }

        public virtual VisitorViewModel BookingVisitor { get; set; }

        public Guid RoomFK { get; set; }

        public virtual RoomViewModel RoomBooking { get; set; }

        public DateTime MoveIn { get; set; }

        public DateTime MoveOut { get; set; }

        public bool CheckedIn { get; set; }
    }
}
