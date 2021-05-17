using Microsoft.EntityFrameworkCore;
using MotorAPI;
using TestWebApplication.MotorAPI.Tests.Models.Services;

namespace TestWebApplication.MotorAPI.Tests
{
    public class InMemoryMotorServiceTest : MotorServiceTests
    {
        public InMemoryMotorServiceTest() : base(
            new DbContextOptionsBuilder<MotorDbContext>().UseInMemoryDatabase("TestDatabase").Options)
        { }
    }
}
