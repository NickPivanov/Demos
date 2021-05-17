using Microsoft.EntityFrameworkCore;
using MotorAPI;
using MotorAPI.Models;
using MotorAPI.Models.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestWebApplication.MotorAPI.Tests.Models.Services
{
    public abstract class MotorPropertyServiceTests
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MotorPropertyService<MotorProperty> service;

        protected MotorPropertyServiceTests(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new MotorPropertyService<MotorProperty>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetPropertiesForMotorTypeAsync()
        {
            //arrange: returns not empty IEnumerable<MotorProperty>
            foreach(MotorType type in Enum.GetValues(typeof(MotorType)))
            {
                //act
                var actual = await service.GetPropertiesForMotorTypeAsync(type);
                //assert
                Assert.NotEmpty(actual);
                Assert.IsAssignableFrom<IEnumerable<MotorProperty>>(actual);
            }
        }
    }
}
