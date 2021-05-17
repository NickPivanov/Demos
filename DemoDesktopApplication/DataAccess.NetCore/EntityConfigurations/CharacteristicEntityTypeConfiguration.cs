using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.NetCore.EntityConfigurations
{
    public class CharacteristicEntityTypeConfiguration : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.Property(p => p.MotorId).IsRequired();
            builder.Property(p => p.MotorPropertyId).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Navigation(p => p.MotorProperty).AutoInclude();
        }
    }
}
