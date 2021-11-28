using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public List<VisitorDTO> AllVisitors();

        public Task<string> AddUser(VisitorDTO user, string password);

        public BookingDTO VisitorBookingInfo(BookingDTO data);

        public Task<VisitorDTO> VerifyUser(string name, string password);

        public Task EditUser(VisitorDTO data);
        public Task DeletUser(VisitorDTO data);
    }
}
