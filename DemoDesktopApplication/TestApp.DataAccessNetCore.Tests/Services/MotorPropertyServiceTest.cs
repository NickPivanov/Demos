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
    public abstract class MotorPropertyServiceTest
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MotorPropertyService<MotorProperty> propertyService;

        protected MotorPropertyServiceTest(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            propertyService = new MotorPropertyService<MotorProperty>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetPropertiesForMotorType()
        {
            int expectedamount = 3;
            var properties = await propertyService.GetPropertiesForMotorTypeAsync(MotorType.Electric);
            Assert.Equal(expectedamount, properties.Count());
        }
    }
}
