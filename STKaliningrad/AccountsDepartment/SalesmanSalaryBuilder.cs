using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDepartment
{
    public class SalesmanSalaryBuilder : SalaryBuilder
    {
        private readonly EmployeeBase _salesman;
        public SalesmanSalaryBuilder(EmployeeBase salesman)
        {
            _salesman = salesman;
            Salary.SalaryBase = _salesman.SalaryBase;
        }

        private double maxExpRate => _salesman.SalaryBase * 0.35;

        public override void SetExpRate(DateTime date)
        {
            double timeIndex = Math.Round((date.Date - _salesman.EmployedDate.Date).TotalDays / 365, 0);
            Salary.ExpRate = (timeIndex * 0.01 * _salesman.SalaryBase) <= maxExpRate ?
               (timeIndex * 0.01 * _salesman.SalaryBase) : maxExpRate;
        }

        public override void SetManagementRate()
        {
            var subordinates = (_salesman as Salesman).Subordinates.ToList();
            double totalSubordinatesSalary = 0;
            foreach (var s in subordinates)
            {
                totalSubordinatesSalary += PersonelExpenses.Where(p => p.Employee.Id == s.Id).FirstOrDefault().CurrentSalary;
            }
            Salary.ManagmentRate = totalSubordinatesSalary * 0.003;
        }
    }
}
