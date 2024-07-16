using BackEndFinal.Data;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
