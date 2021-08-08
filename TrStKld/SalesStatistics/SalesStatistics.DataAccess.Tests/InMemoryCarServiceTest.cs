using DataAccess;
using Microsoft.EntityFrameworkCore;
using SalesStatistics.DataAccess.Tests.Services;

namespace SalesStatistics.DataAccess.Tests
{
    public class InMemoryCarServiceTest : CarServiceTest
    {
        public InMemoryCarServiceTest() : base(
            new DbContextOptionsBuilder<SalesDbContext>()
            .UseInMemoryDatabase("SalesDb").Options)
        {

        }
    }
}
