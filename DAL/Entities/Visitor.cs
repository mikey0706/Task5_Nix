using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Visitor : IdentityUser
    {
        public string VisitorName { get; set; }
        public string Passport { get; set; }

        public bool isAdmin { get; set; }

        public virtual ICollection<Booking> BookingOrders { get; set; }

        public Visitor()
        {
            UserName = VisitorName;
            BookingOrders = new HashSet<Booking>();
        }
    }
}
