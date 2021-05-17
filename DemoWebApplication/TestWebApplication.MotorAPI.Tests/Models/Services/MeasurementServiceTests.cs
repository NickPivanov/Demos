using Microsoft.EntityFrameworkCore;
using MotorAPI;
using MotorAPI.Models;
using MotorAPI.Models.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestWebApplication.MotorAPI.Tests.Models.Services
{
    public abstract class MeasurementServiceTests
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MeasurementService<Measurement> service;

        protected MeasurementServiceTests(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new MeasurementService<Measurement>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetMeasurementsAsync()
        {
            //arrange: returns not empty IEnumerable<Measurements>
            //act
            var actual = await service.GetMeasurementsAsync();
            //assert
            Assert.NotEmpty(actual);
            Assert.IsAssignableFrom<IEnumerable<Measurement>>(actual);
        }
    }
}
