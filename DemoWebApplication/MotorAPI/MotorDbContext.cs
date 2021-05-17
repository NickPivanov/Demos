using Microsoft.EntityFrameworkCore;
using MotorAPI.EntityConfigurations;
using MotorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI
{
    public class MotorDbContext : DbContext
    {
        public MotorDbContext(DbContextOptions<MotorDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            this.EnsureSeedPropertiesTable();
            this.EnsureSeedMotorsData();
        }

        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MotorProperty> MotorProperties { get; set; }
        public DbSet<Motor> Motors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MotorEntityTypeConfiguration().Configure(modelBuilder.Entity<Motor>());
            new CharacteristicEntityTypeConfiguration().Configure(modelBuilder.Entity<Characteristic>());
            new MeasurementEntityTypeConfiguration().Configure(modelBuilder.Entity<Measurement>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
