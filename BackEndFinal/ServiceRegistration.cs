using BackEndFinal.Data;
using BackEndFinal.Repositories;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
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
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISliderContentService, SliderContentService>();
            services.AddScoped<IOfferedAdvantageService, OfferedAdvantageService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IBlogService, BlogService>();
        }
    }
}
