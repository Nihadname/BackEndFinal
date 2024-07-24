using BackEndFinal.Data;
using BackEndFinal.Models;
using BackEndFinal.Repositories;
using BackEndFinal.Services;
using BackEndFinal.Services.interfaces;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ISubscriberService, SubscriberService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherContactInfoService, TeacherContactInfoService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISettingService, SettingService>();
           
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                //options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
