using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        public IEnumerable<T> GetData();

        public void Add(T data);

        public void Update(T data);

        public void Delete(T data);
    }
    
}
