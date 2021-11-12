using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDTO> AllCategories();
        public Task AddCategory(CategoryDTO data, Nullable<DateTime> endDate, int price);
        public Task EditCategory(CategoryDTO data, DateTime startDate, DateTime endDate, int price);
        public Task DeleteCategory(CategoryDTO data);

        public Task<CategoryDTO> FindCategory(string roomId);
    }
}
