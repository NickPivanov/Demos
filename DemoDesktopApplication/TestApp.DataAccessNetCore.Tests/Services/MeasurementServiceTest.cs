using DataAccess.NetCore;
using DataAccess.NetCore.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.DataAccessNetCore.Tests.Services
{
    public abstract class MeasurementServiceTest
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MeasurementService<Measurement> measurementService;

        protected MeasurementServiceTest(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            measurementService = new MeasurementService<Measurement>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetMeasurements()
        {
            var measurements = await measurementService.GetMeasurementsAsync();
            Assert.IsAssignableFrom<IEnumerable<Measurement>>(measurements);
            Assert.NotNull(measurements);
        }
    }
}
