using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CategoryDate
    {
        [Key]
        public Guid CatDateId { get; set; }
        public Guid CategoryFK { get; set; }

        [ForeignKey("CategoryFK")]
        public virtual Category GetCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }

        public CategoryDate()
        {
            CatDateId = Guid.NewGuid();
        }
    }
}
