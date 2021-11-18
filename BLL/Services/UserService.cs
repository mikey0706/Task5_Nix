using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService :IUserService
    {
        private IUnitOfWork _db;
        private IMapper mapper;

        public UserService(IUnitOfWork db)
        {
            _db = db;
            mapper = new Mapper(AutomapperConfig.Config);
        }

        public List<VisitorDTO> AllVisitors()
        {
            return mapper.Map<List<Visitor>, List<VisitorDTO>>(_db.Visitors.GetData().ToList());
        }

        public async Task AddUser(VisitorDTO user, string password)
        {
            try 
            { 
                 var v = mapper.Map<VisitorDTO, Visitor>(user);
                 v.PasswordHash = new PasswordHasher<Visitor>().HashPassword(v, password);
                 _db.Visitors.Add(v);

                 await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }

        public async Task <VisitorDTO> VerifyUser(string name, string password)
        {
            var user = await Task.Run(()=>_db.Visitors.GetData().FirstOrDefault(u => u.VisitorName.Equals(name)));
            if (user != null)
            {
                var check = new PasswordHasher<Visitor>().VerifyHashedPassword(user, user.PasswordHash, password);

                if (check.Equals(PasswordVerificationResult.Success))
                {
                    return mapper.Map<Visitor, VisitorDTO>(user);
                }

            }

            return null;
        }

        public async Task EditUser(VisitorDTO data)
        {
            try 
            { 
                var user =_db.Visitors.GetData().FirstOrDefault(d=>d.Id.Equals(data.Id));

                user.VisitorName = data.VisitorName;
                user.Passport = data.Passport;
             
                _db.Visitors.Update(user);
                await _db.Save();
            
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        public BookingDTO VisitorBookingInfo(BookingDTO data)
        {
            var info = _db.Bookings.GetData().FirstOrDefault(d => d.BookingId == data.BookingId);

            return mapper.Map<Booking, BookingDTO>(info);

        }

        public async Task DeletUser(VisitorDTO data)
        {
            try 
            { 
            var v = _db.Visitors.GetData().FirstOrDefault(d => d.Id.ToString().Equals(data.Id));
            _db.Visitors.Delete(v);
            await _db.Save();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


    }
}
