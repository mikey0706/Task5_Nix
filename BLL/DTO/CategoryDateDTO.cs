using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CategoryDateDTO
    {
        public Guid CatDateId { get; set; }

        public Guid CategoryFK { get; set; }

        public virtual CategoryDTO GetCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }
    }
}
