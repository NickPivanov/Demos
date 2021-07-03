using System;
using System.Threading.Tasks;

namespace AccountsDepartment.Services
{
    public interface IPersonelExpenseService<T>
    {
        public double GetTotalPersonelExpenses();
        public double GetEmployeeCurrentSalary(int id);
        public Task SetEmployeeCurrentSalary(int employee_id, double currentSalary);
        public double CalculateSalaryForEmployee(int id, DateTime date);

    }
}
