using DataAccess;
using Microsoft.EntityFrameworkCore;
using SalesStatistics.DataAccess.Tests.Services;

namespace SalesStatistics.DataAccess.Tests
{
    public class InMemoryOrderServiceTest : OrderServiceTest
    {
        public InMemoryOrderServiceTest() : base(
            new DbContextOptionsBuilder<SalesDbContext>()
            .UseInMemoryDatabase("SalesDb").Options)
        {

        }
    }
}
