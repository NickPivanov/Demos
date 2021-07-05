using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDepartment
{
    public class ManagerSalaryBuilder : SalaryBuilder
    {
        private readonly EmployeeBase _manager;
        public ManagerSalaryBuilder(EmployeeBase manager)
        {
            _manager = manager;
            Salary.SalaryBase = _manager.SalaryBase;
        }

        private double maxExpRate => _manager.SalaryBase * 0.4;

        public override void SetExpRate(DateTime date)
        {
            double timeIndex = Math.Round((date.Date - _manager.EmployedDate.Date).TotalDays / 365, 0);
            Salary.ExpRate = (timeIndex * 0.05 * _manager.SalaryBase) <= maxExpRate ?
               (timeIndex * 0.05 * _manager.SalaryBase) : maxExpRate;
        }

        public override void SetManagementRate()
        {
            var subordinates = (_manager as Manager).Subordinates.Where(n => n.Group == EmployeeGroup.Employees).ToList();
            double totalSubordinatesSalary = 0;
            foreach (var s in subordinates)
            {
                totalSubordinatesSalary += PersonelExpenses.Where(p => p.Employee.Id == s.Id).FirstOrDefault().CurrentSalary;
            }
            Salary.ManagmentRate = totalSubordinatesSalary * 0.005;
        }
    }
}
