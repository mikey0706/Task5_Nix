using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.Models
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<RoomViewModel> CategoryRoom { get; set; }

        public virtual ICollection<CategoryDateViewModel> CategoryDate { get; set; }
    }
}
