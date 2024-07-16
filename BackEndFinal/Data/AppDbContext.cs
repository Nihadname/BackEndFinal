using BackEndFinal.Data.Configurations;
using BackEndFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinal.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<SliderContent> contents { get; set; }
       public DbSet<Setting> settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SliderConfiguration());
            //modelBuilder.ApplyConfiguration(new SliderContentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
