using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class BookInfo
    {
        public string BookingId { get; set; }

        public string VisitorName { get; set; }

        public int RoomNumber { get; set; }

        public DateTime MoveIn { get; set; }

        public DateTime MoveOut { get; set; }

        public bool CheckedIn { get; set; }
    }
}
