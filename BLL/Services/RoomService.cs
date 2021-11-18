using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _db;
        private IMapper mapper;

        public RoomService(IUnitOfWork db)
        {
            _db = db;
            mapper = new Mapper(AutomapperConfig.Config);
        }

        public Task<List<RoomDTO>> AllRooms()
        {
            return Task.Run(()=>mapper.Map<List<Room>, List<RoomDTO>>(_db.Rooms.GetData().ToList()));
        }

        public async Task AddRoom(RoomDTO data)
        {
            try
            {
                var r = mapper.Map<RoomDTO, Room>(data);
                var c = _db.Categories.GetData().FirstOrDefault(d => d.CategoryId == data.CategoryFK);
                r.CategoryFK = c.CategoryId;
                r.RoomCategory = c;
                _db.Rooms.Add(r);
                await _db.Save(); 
                _db.Bookings.Add(new Booking { RoomFK = r.RoomId});
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }

        public async Task EditRoom(RoomDTO data)
        {
            try 
            {
                var r = mapper.Map<RoomDTO, Room>(data);
                _db.Rooms.Update(r);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task DeleteRoom(string roomId)
        {
            try
            {
                var id = Guid.Parse(roomId);
                var rm = _db.Rooms.GetData().FirstOrDefault(d=>d.RoomId==id);
                _db.Rooms.Delete(rm);
                await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public Task<IEnumerable<RoomDTO>> UserRooms(string name)
        {
            var bks = _db.Visitors.GetData().FirstOrDefault(d => d.VisitorName.Equals(name)).BookingOrders;

            var rooms = new List<Room>();

            foreach (var item in bks)
            {
                rooms.Add(_db.Bookings.GetData().FirstOrDefault(d => d.BookingId == item.BookingId).RoomBooking);
            }

            var res = mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(rooms);

            return Task.Run(() => res);
        }

        public Task<List<RoomDTO>> RoomsByDate(DateTime date) 
        {
            var bks = _db.Bookings.GetData().Where(d=>d.MoveIn.Date > date.Date).Select(k=>k.RoomFK).Distinct();
            var rooms = new List<Room>();
            foreach (var item in bks) 
            {
                rooms.Add(_db.Rooms.GetData().FirstOrDefault(d=>d.RoomId == item));
            }

            return Task.Run(()=>mapper.Map<List<Room>, List<RoomDTO>>(rooms));
        }

    }
}
