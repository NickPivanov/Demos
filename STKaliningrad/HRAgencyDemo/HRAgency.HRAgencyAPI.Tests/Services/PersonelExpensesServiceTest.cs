using AccountsDepartment.Services;
using DataAccess;
using DataAccess.Models;
using DataAccess.Services;
using HRAgencyAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HRAgency.HRAgencyAPI.Tests.Services
{
    public class PersonelExpensesServiceTest
    {
        protected DbContextOptions<HRAgencyDBContext> ContextOptions { get; }
        protected IPersonelExpenseService<EmployeeBase> expenseService;
        protected IEmployeeService<EmployeeBase> employeeService;

        public PersonelExpensesServiceTest(DbContextOptions<HRAgencyDBContext> contextOptions)
        {
            ContextOptions = contextOptions;
            var context = new HRAgencyDBContext(ContextOptions);
            HRAgencyDBContextExtensions.SeedEmployeeData(context);
            employeeService = new EmployeeService<EmployeeBase>(context);
            expenseService = new PersonelExpenseService<EmployeeBase>(context);

        }

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await new HRAgencyDBContext(ContextOptions).Database.CanConnectAsync());
        }

        [Fact]
        public void EmployeeCalcSalaryTest()
        {
            //arrange
            double expected = 103;

            //act
            var result = expenseService.CalculateSalaryForEmployee(1, DateTime.Today.Date);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCalcSalaryTest()
        {
            //arrange
            double expected = 173.02;

            //act
            var m = await employeeService.GetByIdAsync(2);
            (m as Manager).Subordinates.Add(await employeeService.GetByIdAsync(1));
            var result = expenseService.CalculateSalaryForEmployee(2, DateTime.Today.Date);

            //assert
            Assert.IsAssignableFrom<Manager>(m);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task SalesmanCalcSalaryTest()
        {
            //arange
            double expected = 206.83;

            //act
            var s = await employeeService.GetByIdAsync(3);
            (s as Salesman).Subordinates.AddRange(new[] {
                await employeeService.GetByIdAsync(1),
                await employeeService.GetByIdAsync(2) });
            var result = expenseService.CalculateSalaryForEmployee(3, DateTime.Today.Date);

            //assert
            Assert.IsAssignableFrom<Salesman>(s);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Can_GetEmployeeCurrentSalary()
        {
            //arrange
            double expected = 103;
            //act
            var e = await employeeService.GetByIdAsync(1);
            var result = expenseService.GetEmployeeCurrentSalary(1);
            //assert
            Assert.True(result > double.MinValue);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Can_SetEmployeeCurrentSalary()
        {
            //arrange
            double expected = 106;
            //act
            await expenseService.SetEmployeeCurrentSalary(1, 106);
            var currentSalary = expenseService.GetEmployeeCurrentSalary(1);
            //assert
            Assert.Equal(expected, currentSalary);
        }

        [Fact]
        public void Can_GetTotalExpense()
        {
            //arrange
            double expected = 486.97;
            //act
            var result = expenseService.GetTotalPersonelExpenses();
            //assert
            Assert.Equal(expected, result);
        }
    }
}
