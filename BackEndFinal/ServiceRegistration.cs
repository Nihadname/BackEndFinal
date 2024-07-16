using BackEndFinal.Data;
using BackEndFinal.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Repositories.interfaces;

namespace BackEndFinal
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"))
            );
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
