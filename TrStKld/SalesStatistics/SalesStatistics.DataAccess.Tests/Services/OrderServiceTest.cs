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
    public class OrderServiceTest
    {
        protected DbContextOptions<SalesDbContext> ContextOptions { get; }
        protected OrderService<Order> service;

        public OrderServiceTest(DbContextOptions<SalesDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            service = new OrderService<Order>(new SalesDbContext(ContextOptions));
        }

        [Fact]
        public async Task Can_GetAllOrders()
        {
            //arrange
            bool expected = true;
            //act
            var orders = await service.GetAllAsync();
            //assert
            Assert.IsAssignableFrom<IEnumerable<Order>>(orders);
            Assert.Equal(expected, orders.Any());
        }

        [Fact]
        public async Task Can_GetOrderById()
        {
            //arrange
            string expectedName = "BMW";
            //act
            var order = await service.GetByIdAsync(1);
            //assert
            Assert.Equal(expectedName, order.Car.Brand);
        }

    }
}
