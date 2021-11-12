using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryDate
    {
        public IEnumerable<CategoryDateDTO> AllCatDate();
        public Task AddCatDate(CategoryDateDTO data);
        public Task DeleteCatDate(CategoryDateDTO data);
        public Task EditCatDate(Guid id, CategoryDateDTO data);
    }
}
