using BackEndFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndFinal.Data.Configurations
{
    public class SliderContentConfiguration : IEntityTypeConfiguration<SliderContent>
    {
        public void Configure(EntityTypeBuilder<SliderContent> builder)
        {
            foreach (var property in typeof(SliderContent).GetProperties().Where(p => p.PropertyType == typeof(string)))
            {
                builder.Property(property.Name).IsRequired();
            }
            builder.Property(s => s.CreatedTime).HasDefaultValueSql("GETDATE()");


        }
    }
}
