using DataAccess;
using DataAccess.Services;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SalesStatistics.DataAccess.Tests.Services
{
    public class CarServiceTest
    {
        protected DbContextOptions<SalesDbContext> ContextOptions { get; }
        protected CarService<Car> service;

        public CarServiceTest(DbContextOptions<SalesDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new CarService<Car>(new SalesDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetAllCars()
        {
            //arrange
            bool expected = true;
            //act
            var cars = await service.GetAllAsync();
            //assert
            Assert.IsAssignableFrom<IEnumerable<Car>>(cars);
            Assert.Equal(expected, cars.Any());
        }

        [Fact]
        public async Task Can_GetCarById()
        {
            //arrange
            string expectedName = "BMW";
            //act
            var car = await service.GetByIdAsync(1);
            //assert
            Assert.Equal(expectedName, car.Brand);
        }
    }
}
