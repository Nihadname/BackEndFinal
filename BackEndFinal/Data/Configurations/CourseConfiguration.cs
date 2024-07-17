using BackEndFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndFinal.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            foreach (var property in typeof(Course).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                builder.Property(property.Name).IsRequired();
            }
            builder.Property(s=> s.CreatedTime).HasDefaultValueSql("GETDATE()");
        }
    }
}
