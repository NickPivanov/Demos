using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorAPI.Models;

namespace MotorAPI.EntityConfigurations
{
    public class MotorEntityTypeConfiguration : IEntityTypeConfiguration<Motor>
    {
        public void Configure(EntityTypeBuilder<Motor> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Type).IsRequired();

            builder.HasMany(c => c.Characteristics).WithOne(m => m.Motor);
            builder.HasMany(m => m.MeasurementsLog).WithOne(m => m.Motor);
        }
    }
}
