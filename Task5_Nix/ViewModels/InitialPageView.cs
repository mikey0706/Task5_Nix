using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class InitialPageView
    {
        private DateTime deffaultDate = DateTime.Now.Date;

        [Required]
        public DateTime CheckIn 
        { 
            get { return deffaultDate; } 
            set { deffaultDate = value; } 
        }
        
        [Required]
        public DateTime CheckOut
        {
            get { return deffaultDate; }
            set { deffaultDate = value; }
        }

        public IEnumerable<RoomInfo> Rooms { get; set; }

        public InitialPageView() 
        {
            CheckIn = DateTime.Now.Date;
            CheckOut = DateTime.Now.Date;
        }
    }
}
