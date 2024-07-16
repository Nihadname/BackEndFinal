using BackEndFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndFinal.Data.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(s => s.Key)
                  .IsRequired();

            builder.HasIndex(s => s.Key)
                   .IsUnique();
        }
    }
}
