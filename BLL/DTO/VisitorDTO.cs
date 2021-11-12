using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class VisitorDTO
    {
        public string Id { get; set; }
        public string VisitorName { get; set; }

        public string Passport { get; set; }

        public bool isAdmin { get; set; }

        public virtual IEnumerable<BookingDTO> BookingOrders { get; set; }

    }
}
