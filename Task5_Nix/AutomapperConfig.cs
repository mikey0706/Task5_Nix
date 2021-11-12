using AutoMapper;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;

namespace Task5_Nix
{
    static class AutomapperConfig
    {
        internal static MapperConfiguration Config
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<VisitorViewModel, VisitorDTO>();
                    cfg.CreateMap<VisitorDTO, VisitorViewModel>();

                    cfg.CreateMap<BookingViewModel, BookingDTO>();
                    cfg.CreateMap<BookingDTO, BookingViewModel>();

                    cfg.CreateMap<RoomViewModel, RoomDTO>();
                    cfg.CreateMap<RoomDTO, RoomViewModel>();

                    cfg.CreateMap<CategoryViewModel, CategoryDTO>();
                    cfg.CreateMap<CategoryDTO, CategoryViewModel>();

                    cfg.CreateMap<CategoryDateViewModel, CategoryDateDTO>();
                    cfg.CreateMap<CategoryDateDTO, CategoryDateViewModel>();

                });

            }
        }
    }
}
