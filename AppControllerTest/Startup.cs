using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControllerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            var configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HotelAppContext>(options => options.UseSqlServer(connectionString));


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
