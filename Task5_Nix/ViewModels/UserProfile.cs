using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class UserProfile
    {
        public string Id { get; set; }

        public string VisitorName { get; set; }

        public string PassportSeries { get; set; }

        public string PassportNum { get; set; }

        public IEnumerable<RoomInfo> UserRooms { get; set; }
    }
}
