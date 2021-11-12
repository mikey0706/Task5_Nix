using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static class AutomapperConfig
    {
        internal static MapperConfiguration Config
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Visitor, VisitorDTO>();
                    cfg.CreateMap<Room, RoomDTO>();
                    cfg.CreateMap<Booking, BookingDTO>();
                    cfg.CreateMap<Category, CategoryDTO>();
                    cfg.CreateMap<CategoryDate, CategoryDateDTO>();

                    cfg.CreateMap<VisitorDTO, Visitor>();
                    cfg.CreateMap<RoomDTO, Room>();
                    cfg.CreateMap<BookingDTO, Booking>();
                    cfg.CreateMap<CategoryDTO, Category>();
                    cfg.CreateMap<CategoryDateDTO, CategoryDate>();
                }
                );

            }
        }
    }
}
