using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }

        public int RoomNumber { get; set; }

        public Guid CategoryFK { get; set; }

        [ForeignKey("CategoryFK")]
        public virtual Category RoomCategory { get; set; }

        public virtual ICollection<Booking> RoomBooking { get; set; }
        public Room()
        {
            RoomBooking = new HashSet<Booking>();
            RoomId = Guid.NewGuid();
        }
    }
}
