using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<RoomDTO> CategoryRoom { get; set; }

        public virtual ICollection<CategoryDateDTO> CategoryDate { get; set; }
    }
}
