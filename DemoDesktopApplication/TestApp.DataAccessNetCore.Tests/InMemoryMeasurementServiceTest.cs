using DataAccess.NetCore;
using Microsoft.EntityFrameworkCore;
using TestApp.DataAccessNetCore.Tests.Services;

namespace TestApp.DataAccessNetCore.Tests
{
    public class InMemoryMeasurementServiceTest : MeasurementServiceTest
    {
        public InMemoryMeasurementServiceTest() : base(
            new DbContextOptionsBuilder<MotorDbContext>()
            .UseInMemoryDatabase("MotorsDb").Options)
        { }
    }
}
