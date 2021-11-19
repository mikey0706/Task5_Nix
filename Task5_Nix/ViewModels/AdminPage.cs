using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;

namespace Task5_Nix.ViewModels
{
    public class AdminPage
    {
        public IEnumerable<RoomInfo> AdminRooms { get; set; }

        public int MonthProfit { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
