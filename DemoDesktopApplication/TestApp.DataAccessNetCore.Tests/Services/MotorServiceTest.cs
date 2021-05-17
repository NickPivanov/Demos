using DataAccess.NetCore;
using DataAccess.NetCore.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.DataAccessNetCore.Tests.Services
{
    public abstract class MotorServiceTest
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MotorService<Motor> service;

        protected MotorServiceTest(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new MotorService<Motor>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetAllMototrs()
        {
            //arrange
            bool expected = true;
            //act
            var motors = await service.GetAllAsync();
            //assert
            Assert.IsAssignableFrom<IEnumerable<Motor>>(motors);
            Assert.Equal(expected, motors.Any());
        }

        [Fact]
        public async Task Can_GetMotorById()
        {
            //arrange
            string expectedName = "Motor1";
            //act
            var motor = await service.GetByIdAsync(1);
            //assert
            Assert.Equal(expectedName, motor.Name);
        }

        [Fact]
        public void Exception_GetMotorById()
        {
            //arrange: returns exception if motor doesn't exist in database
            //act
            var act = service.GetByIdAsync(99).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(act);
        }

        [Fact]
        public async Task Can_AddNew()
        {
            //arrange
            bool expected = true;
            //act
            var actual = await service.AddNewAsync(new Motor { Name = "actual", Type = MotorType.Electric});
            //assert
            Assert.Equal(expected, !string.IsNullOrEmpty(actual.Id.ToString()));
        }

        [Fact]
        public void Exception_AddNew()
        {
            //arrange: new item is null returns exeption
            //new item's name is null or empty returns exeption
            var expected = new Motor();
            //act
            var motor_is_nullable = service.AddNewAsync(null).Exception;
            var motorname_is_null = service.AddNewAsync(expected).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(motor_is_nullable);
            Assert.IsAssignableFrom<Exception>(motorname_is_null);
        }

        [Fact]
        public async Task Can_DeleteMotor()
        {
            //arrange
            var expected = 3;
            //act
            var result = await service.DeleteAsync(3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Exception_DeleteMotor()
        {
            //arrange: if delete succeded, returns null. Else - exeption
            var result = service.DeleteAsync(5).Exception;
            Assert.IsAssignableFrom<Exception>(result);
        }

        [Fact]
        public async Task Can_Update()
        {
            //arrange
            var motor = await service.GetByIdAsync(1);
            //act
            motor.Name = "NewName";
            var result = await service.UpdateAsync(motor);
            //assert
            Assert.Equal(result, motor);
        }

        [Fact]
        public void Exception_Update()
        {
            //arrange: if motor that is expected to be updated is null, returns Exception
            var motor = new Motor();
            var result = service.UpdateAsync(motor).Exception;
            Assert.IsAssignableFrom<Exception>(result);
        }

        
    }
}
