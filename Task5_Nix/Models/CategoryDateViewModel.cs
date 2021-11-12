using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.Models
{
    public class CategoryDateViewModel
    {
        public Guid CatDateId { get; set; }
        public Guid CategoryFK { get; set; }

        public virtual CategoryViewModel GetCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }
    }
}
