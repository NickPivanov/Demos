using AccountsDepartment;
using AccountsDepartment.Services;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRAgencyAPI.Services
{
    public class PersonelExpenseService<T> : IPersonelExpenseService<T> where T : EmployeeBase
    {
        protected readonly HRAgencyDBContext db;
        public PersonelExpenseService(HRAgencyDBContext context)
        {
            db = context;
        }

        public double CalculateSalaryForEmployee(int id, DateTime date)
        {
            var employee = db.PersonelExpenses.FirstOrDefault(p => p.Employee.Id == id).Employee;
            if (employee is null)
                throw new Exception("No salary data in databse. Set employee current salary first");

            SalaryBuilder builder = employee.Group switch
            { 
                EmployeeGroup.Employees => new EmployeeSalaryBuilder(employee),
                EmployeeGroup.Management => new ManagerSalaryBuilder(employee),
                EmployeeGroup.Sales => new SalesmanSalaryBuilder(employee),
                _ => new EmployeeSalaryBuilder(employee)
            };

            builder.PersonelExpenses = db.PersonelExpenses.ToList();
            builder.SetExpRate(date);
            builder.SetManagementRate();
            return builder.Salary.CalculateSalary();
        }

        public double GetEmployeeCurrentSalary(int id)
        {
            double? current = db.PersonelExpenses
                .FirstOrDefault(p => p.Employee.Id == id)?.CurrentSalary;
            if (!current.HasValue)
            {
                throw new Exception("Current salary has not been set yet for this employee");
            }
            return current.Value;
        }

        public async Task SetEmployeeCurrentSalary(int id, double currentSalary)
        {
            if(!db.PersonelExpenses.Any(p => p.Employee.Id == id))
            {
                var e = await db.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                db.PersonelExpenses.Add(
                    new PersonelExpense {Employee = e, CurrentSalary = currentSalary });
                await db.SaveChangesAsync();
            }
            
            var expense = db.PersonelExpenses.FirstOrDefault(e => e.Employee.Id == id);
            expense.CurrentSalary = currentSalary;
            db.Update(expense);
            await db.SaveChangesAsync();
        }

        public double GetTotalPersonelExpenses()
        {
            var sum = db.PersonelExpenses.Sum(s => s.CurrentSalary);
            return Math.Round(sum, 2);
        }

    }
}
