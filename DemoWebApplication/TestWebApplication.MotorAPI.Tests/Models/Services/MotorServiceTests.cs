using Microsoft.EntityFrameworkCore;
using MotorAPI;
using MotorAPI.Models;
using MotorAPI.Models.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestWebApplication.MotorAPI.Tests.Models.Services
{
    public abstract class MotorServiceTests
    {
        protected DbContextOptions<MotorDbContext> ContextOptions { get; }
        protected MotorService<Motor> service;

        protected MotorServiceTests(DbContextOptions<MotorDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new MotorService<Motor>(new MotorDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_AddNewMotor()
        {
            //arrange
            Motor expected = new() {Name = "Excpected", Type = MotorType.Combustion };
            //act
            var actual = await service.AddNewAsync(expected);
            //assert
            Assert.True(!string.IsNullOrEmpty(actual.Id.ToString()));
        }

        [Fact]
        public void Exception_On_AddNewMotor()
        {
            //arrange
            Motor expected = new();
            Motor nullable = null;
            //act
            var actual = service.AddNewAsync(expected).Exception;
            var actual_nullable = service.AddNewAsync(nullable).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(actual);
            Assert.IsAssignableFrom<Exception>(actual_nullable);
        }

        [Fact]
        public async Task Can_GetAllMototrs()
        {
            //arrange: should return IEnumerable<Motor>, that contains elements
            //act
            var actual = await service.GetAllAsync();
            //assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task Can_GetById()
        {
            //arrange
            int expectedId = 1;
            //act
            var motor = await service.GetByIdAsync(1);
            //assert
            Assert.Equal(expectedId, motor.Id);
        }

        [Fact]
        public void Exception_On_GetById()
        {
            //arrange: returns exception if motor doesn't exist in database
            //act
            var excpected = service.GetByIdAsync(99).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(excpected);
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
        public void Exception_On_UpdateMotor()
        {
            //arrange
            Motor expected = new() { Name="This one doesn't exist in DB", Type = MotorType.Hydraulic };
            expected.Name = "Because it has no Id"; 
            //act
            var actual = service.UpdateAsync(expected).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(actual);
        }

        [Fact]
        public async Task Can_DeleteMotor()
        {
            //arrange: after deleting will throw exception on calling GetByIdAsync
            //act
            await service.DeleteAsync(2);
            var result = service.GetByIdAsync(2).Exception;// will be 'null' if completed successfully
            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Exception_On_DeleteMotor()
        {
            //arrange: after deleting will throw exception on calling GetByIdAsync
            //act
            var excpected = service.DeleteAsync(99).Exception;
            //assert
            Assert.IsAssignableFrom<Exception>(excpected);
        }
    }
}
