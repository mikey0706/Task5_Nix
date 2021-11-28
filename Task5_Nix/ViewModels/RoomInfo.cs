using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class RoomInfo
    {
        public string Id { get; set; }

        public Guid BookingId { get; set; }
        public int RoomNumber { get; set; }

        public string RoomCategory { get; set; }

        public int Price { get; set; }

        public RoomInfo() { }

        public RoomInfo(RoomDTO room, CategoryDateDTO cd, ICategoryService cs) 
        {

            Id = room.RoomId.ToString();
            RoomNumber = room.RoomNumber;
            RoomCategory = cs.AllCategories()
                            .FirstOrDefault(c => c.CategoryId == cd.CategoryFK).CategoryName;
            Price = cd.Price;
        }

    }
}
