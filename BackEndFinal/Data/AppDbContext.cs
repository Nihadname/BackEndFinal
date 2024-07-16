using BackEndFinal.Data.Configurations;
using BackEndFinal.Models;
using BackEndFinal.Models.Common;
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
        public DbSet<Footer> footers { get; set; }
        public DbSet<FooterContent> footerContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SliderConfiguration());
            //modelBuilder.ApplyConfiguration(new SliderContentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
