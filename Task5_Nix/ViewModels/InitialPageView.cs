using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class InitialPageView
    {

        [Required]
        public DateTime CheckIn { get;set;}
        
        [Required]
        public DateTime CheckOut {get;set;}

        public IEnumerable<RoomInfo> Rooms { get; set; }

    }
}
