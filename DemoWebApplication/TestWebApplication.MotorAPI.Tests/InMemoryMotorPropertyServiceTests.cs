using Microsoft.EntityFrameworkCore;
using MotorAPI;
using TestWebApplication.MotorAPI.Tests.Models.Services;

namespace TestWebApplication.MotorAPI.Tests
{
    public class InMemoryMotorPropertyServiceTests : MotorPropertyServiceTests
    {
        public InMemoryMotorPropertyServiceTests() : base
            (new DbContextOptionsBuilder<MotorDbContext>().UseInMemoryDatabase("TestDatabase").Options)
        {

        }
    }
}
