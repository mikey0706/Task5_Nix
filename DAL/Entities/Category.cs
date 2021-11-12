using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Room> CategoryRoom { get; set; }

        public virtual ICollection<CategoryDate> CategoryDate { get; set; }

        public Category()
        {
            CategoryId = Guid.NewGuid();

            CategoryRoom = new HashSet<Room>();

            CategoryDate = new HashSet<CategoryDate>();
        }
    }
}
