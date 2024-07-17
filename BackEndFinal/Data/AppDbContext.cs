using BackEndFinal.Data.Configurations;
using BackEndFinal.Data.Migrations;
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
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseImage> courseImages { get; set; }
        public DbSet<CourseTeacher> coursesTeachers { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<OfferedAdvantages> OfferedAdvantages {  get; set; }
        public DbSet<WhyChoose> whyChooses  { get; set; }
        public DbSet<EventImage> eventImages { get; set; }
        public DbSet<BackEndFinal.Models.TestimonialArea> testimonialAreas { get; set; }
        public DbSet<BlogImage> blogImages  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SliderConfiguration());
            //modelBuilder.ApplyConfiguration(new SliderContentConfiguration());
            modelBuilder.Entity<Slider>()
               .HasOne(s => s.SliderContent)
               .WithOne(sc => sc.Slider)
               .HasForeignKey<SliderContent>(sc => sc.SliderId);

           

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
