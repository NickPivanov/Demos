using DataAccess.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDepartment
{
    public abstract class SalaryBuilder
    {
        public Salary Salary;
        public List<PersonelExpense> PersonelExpenses { get; set; } = new();
        public SalaryBuilder()
        {
            Salary = new();
        }
        
        public abstract void SetExpRate(DateTime date);
        public abstract void SetManagementRate();
    }
}
