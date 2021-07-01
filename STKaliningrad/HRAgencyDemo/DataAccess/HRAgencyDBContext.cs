using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class HRAgencyDBContext : DbContext
    {
        public HRAgencyDBContext(DbContextOptions<HRAgencyDBContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            this.SeedEmployeeData();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<PersonelExpense> PersonelExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(p => p.EmployedDate).IsRequired();

            modelBuilder.Entity<Manager>().Navigation(p => p.Subordinates);
            modelBuilder.Entity<Manager>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Manager>().Property(p => p.EmployedDate).IsRequired();

            modelBuilder.Entity<Salesman>().Navigation(p => p.Subordinates);
            modelBuilder.Entity<Salesman>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Salesman>().Property(p => p.EmployedDate).IsRequired();

            modelBuilder.Entity<PersonelExpense>().Navigation(p => p.Employee).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}
