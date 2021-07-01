using DataAccess;
using DataAccess.Models;
using DataAccess.Services;
using HRAgencyAPI;
using HRAgencyAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HRAgency.HRAgencyAPI.Tests.Services
{
    public class EmployeeServiceTest
    {
        protected DbContextOptions<HRAgencyDBContext> ContextOptions { get; }
        protected IEmployeeService<EmployeeBase> employeeService;

        public EmployeeServiceTest(DbContextOptions<HRAgencyDBContext> contextOptions)
        {
            ContextOptions = contextOptions;
            var context = new HRAgencyDBContext(ContextOptions);
            HRAgencyDBContextExtensions.SeedEmployeeData(context);
            employeeService = new EmployeeService<EmployeeBase>(context);

        }

        [Fact]
        public async Task Can_GetAllEmploees()
        {
            //arrange
            int expected = 3;
            //act
            var result = await employeeService.GetAllAsync();
            //assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task Can_GetEmployeeById()
        {
            //arrange
            int expected = 1;

            //act
            var result = await employeeService.GetByIdAsync(1);

            //assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        public async Task Can_AddNewEmployee()
        {
            //arrage
            int expected = (await employeeService.GetAllAsync()).Count() + 1;
            //act
            var e = new EmployeeFactory(EmployeeGroup.Employees).CreateEmployee("Ahsoka", DateTime.Today);
            var result = await employeeService.AddNewAsync(e);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Can_UpdateEmployee()
        {
            //arrange
            string expected = "Anakin Skywalker";
            //act
            var e = await employeeService.GetByIdAsync(1);
            e.Name = expected;
            await employeeService.UpdateAsync(e);
            var result = await employeeService.GetByIdAsync(1);
            //assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task Can_ManageSubordinates()
        {
            //arrange
            int expected = 1;
            //act
            await employeeService.AssignSubordinateForEmployee(2, 1);
            var result = await employeeService.GetSubordinatesForEmployee(2);
            //assert
            Assert.Equal(expected, result.Count());
        }

        [Fact]
        public async Task Can_RemoveSubordinate()
        {
            //arrange
            int expected = 0;
            //act
            await employeeService.RemoveSubordinateForEmployee(2, 1);
            var result = await employeeService.GetSubordinatesForEmployee(2);
            //assert
            Assert.Equal(expected, result.Count());
        }
    }
}
