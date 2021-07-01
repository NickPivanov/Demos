using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class HRAgencyDBContextExtensions
    {
        public static void SeedEmployeeData(this HRAgencyDBContext context)
        {
            if (!context.Set<Employee>().Any())
            {
                var employee = new Employee
                {
                    Name = "Anakin",
                    EmployedDate = DateTime.Today.AddYears(-1).AddDays(-2)
                };

                context.Set<Employee>().Add(employee);
            }

            if (!context.Set<Manager>().Any())
            {
                var manager = new Manager
                {
                    Name = "Obi Van",
                    EmployedDate = DateTime.Today.AddYears(-3).AddDays(-2)
                };
                context.Set<Manager>().Add(manager);
            }

            if (!context.Set<Salesman>().Any())
            {
                var salesman = new Salesman
                { 
                    Name = "Master Yoda",
                    EmployedDate = DateTime.Today.AddYears(-5).AddDays(-2)
                };
                context.Set<Salesman>().Add(salesman);
            }

            context.SaveChanges();

            if (!context.PersonelExpenses.Any())
            {
                context.PersonelExpenses.AddRange(
                    new PersonelExpense
                    {
                        Employee = context.Employees.First(), 
                        CurrentSalary = 103
                    },
                    new PersonelExpense
                    { 
                        Employee = context.Managers.First(),
                        CurrentSalary = 173.15
                    },
                    new PersonelExpense
                    {
                        Employee = context.Salesmen.First(),
                        CurrentSalary = 210.82
                    }
                 );
                context.SaveChanges();
            }
        }

        
           
        
    }
}
