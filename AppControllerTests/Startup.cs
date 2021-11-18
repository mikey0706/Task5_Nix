using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppControllerTests
{
    public class Startup 
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddHttpContextAccessor();

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
