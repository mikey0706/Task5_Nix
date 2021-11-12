using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        public string VisitorFK { get; set; }

        [ForeignKey("VisitorFK")]
        public virtual Visitor BookingVisitor { get; set; }

        public Guid RoomFK { get; set; }

        [ForeignKey("RoomFK")]
        public virtual Room RoomBooking { get; set; }

        public DateTime MoveIn { get; set; }

        public DateTime MoveOut { get; set; }

        public bool CheckedIn { get; set; }

        public Booking()
        {
            BookingId = Guid.NewGuid();
        }

    }
}
