using DataAccess.NetCore;
using Microsoft.EntityFrameworkCore;
using TestApp.DataAccessNetCore.Tests.Services;

namespace TestApp.DataAccessNetCore.Tests
{
    public class InMemoryMotorServiceTest : MotorServiceTest
    {
        public InMemoryMotorServiceTest() : base(
            new DbContextOptionsBuilder<MotorDbContext>()
            .UseInMemoryDatabase("MotorsDb").Options)
        { }
    }
}
