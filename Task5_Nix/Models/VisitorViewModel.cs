using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.Models
{
    public class VisitorViewModel
    {
        public string Id { get; set; }
        public string VisitorName { get; set; }

        public bool isAdmin { get; set; }

        public string Role { get; set; }

        public string Passport { get; set; }

        public virtual IEnumerable<BookingViewModel> BookingOrders { get; set; }

        public VisitorViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
