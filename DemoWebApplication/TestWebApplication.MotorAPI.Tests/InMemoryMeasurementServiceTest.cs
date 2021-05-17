using Microsoft.EntityFrameworkCore;
using MotorAPI;
using TestWebApplication.MotorAPI.Tests.Models.Services;

namespace TestWebApplication.MotorAPI.Tests
{
    public class InMemoryMeasurementServiceTest : MeasurementServiceTests
    {
        public InMemoryMeasurementServiceTest() : base(
            new DbContextOptionsBuilder<MotorDbContext>().UseInMemoryDatabase("TestDatabase").Options)
        { }
    }
}
