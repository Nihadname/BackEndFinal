using BackEndFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndFinal.Data.Configurations
{
    public class TestimonialAreaConfiguration : IEntityTypeConfiguration<TestimonialArea>
    {
        public void Configure(EntityTypeBuilder<TestimonialArea> builder)
        {
            foreach (var property in typeof(TestimonialArea).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                builder.Property(property.Name).IsRequired();
            }
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");

        }
    }
}
