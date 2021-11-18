using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Context
{
    public class HotelAppContext : IdentityDbContext
    {
        public HotelAppContext(DbContextOptions<HotelAppContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<CategoryDate> CategoryDates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = new Visitor()
            {
                VisitorName = "Admin",
                isAdmin = true
            };

            var p = new PasswordHasher<Visitor>().HashPassword(user, "12345admin");
            user.PasswordHash = p;


            var r = new Room()
            {
                RoomNumber = 1
            };

            var r1 = new Room()
            {
                RoomNumber = 2
            };

            var r2 = new Room()
            {
                RoomNumber = 3
            };

            var c = new Category()
            {
                CategoryName = "Lux"
            };

            var mid = new Category()
            {
                CategoryName = "Middle"
            };
            var b = new Booking()
            {
                VisitorFK = user.Id,
                RoomFK = r.RoomId,
                MoveIn = new DateTime(2021, 11, 01),
                MoveOut = new DateTime(2021, 11, 20)
            };

            var b2 = new Booking()
            {
                RoomFK = r1.RoomId,
                MoveIn = new DateTime(2021, 11, 01),
                MoveOut = new DateTime(2021, 11, 20)
            };

            var b3 = new Booking()
            {
                RoomFK = r2.RoomId,
                MoveIn = new DateTime(2021, 11, 01),
                MoveOut = new DateTime(2021, 11, 20)
            };

            var cd = new CategoryDate()
            {
                CategoryFK = c.CategoryId,
                StartDate = new DateTime(2021, 11, 01),
                EndDate = new DateTime(2021, 11, 20),
                Price = 2099
            };

            var cdm = new CategoryDate()
            {
                CategoryFK = mid.CategoryId,
                StartDate = new DateTime(2021, 11, 01),
                EndDate = new DateTime(2021, 11, 20),
                Price = 1099
            };

            var role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "admin"
            };

            r.CategoryFK = c.CategoryId;
            r1.CategoryFK = c.CategoryId;
            r2.CategoryFK = mid.CategoryId;


            builder.Entity<Visitor>().HasMany(d => d.BookingOrders)
                .WithOne(o => o.BookingVisitor);

            builder.Entity<Room>().HasMany(d => d.RoomBooking)
                .WithOne(rb => rb.RoomBooking);

            builder.Entity<Category>().HasMany(d => d.CategoryRoom)
                .WithOne(cr => cr.RoomCategory).IsRequired();

            builder.Entity<Category>().HasMany(d => d.CategoryDate)
                .WithOne(cd => cd.GetCategory).IsRequired();

            builder.Entity<IdentityRole>().HasData(role);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id
                }
                );

            builder.Entity<Visitor>().HasData(user);
            builder.Entity<Room>().HasData(r, r1, r2);
            builder.Entity<Booking>().HasData(b, b2, b3);
            builder.Entity<Category>().HasData(c, mid);
            builder.Entity<CategoryDate>().HasData(cd, cdm);
        }
    }
}
