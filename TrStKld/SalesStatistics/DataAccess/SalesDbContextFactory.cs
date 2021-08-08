using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SalesDbContextFactory
    {
        private readonly string _connectionString;
        public SalesDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SalesDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SalesDbContext>();
            options.UseSqlite(_connectionString);

            return new SalesDbContext(options.Options);
        }
    }
}
