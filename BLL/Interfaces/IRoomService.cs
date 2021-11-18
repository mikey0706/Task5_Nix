using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        public Task<List<RoomDTO>> AllRooms();
        public Task AddRoom(RoomDTO data);
        public Task EditRoom(RoomDTO data);
        public Task DeleteRoom(string roomId);
        public Task<IEnumerable<RoomDTO>> UserRooms(string name);
        public Task<List<RoomDTO>> RoomsByDate(DateTime date);
    }
}
