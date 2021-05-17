using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.NetCore.EntityConfigurations
{
    public class MeasurementEntityTypeConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.Property(p => p.Time).IsRequired();
            builder.Property(p => p.MotorId).IsRequired();
            builder.Property(p => p.MotorPropertyId).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Navigation(p => p.MotorProperty).AutoInclude();
        }
    }
}
