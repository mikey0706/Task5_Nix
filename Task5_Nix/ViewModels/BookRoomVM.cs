using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Task5_Nix.ViewModels
{
    public class BookRoomVM
    {
        private DateTime deffaultDate = DateTime.Now.Date;
        public string BookingId { get; set; }

        public string VisitorFk { get; set; }

        public string VisitorName { get;set; }

        public Guid RoomFK { get; set; }

        public int RoomNumber { get; set; }

        
        public DateTime MoveIn 
        { 
            get { return deffaultDate; } 
            set { deffaultDate = value; } 
        }

        public DateTime MoveOut { 
            get { return deffaultDate; } 
            set { deffaultDate = value; } 
        }

        public int Price { get; set; }

        public bool CheckedIn { get; set; }

    }
}
