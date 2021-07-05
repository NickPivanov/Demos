using DataAccess.Models;
using System;

namespace AccountsDepartment.Services
{
    public class EmployeeSalaryBuilder : SalaryBuilder
    {
        private readonly EmployeeBase _employee;
        public EmployeeSalaryBuilder(EmployeeBase employee)
        {
            _employee = employee;
            Salary.SalaryBase = _employee.SalaryBase;
        }
        private double maxExpRate => _employee.SalaryBase * 0.3;

        public override void SetExpRate(DateTime date)
        {
            double timeIndex = Math.Round((date.Date - _employee.EmployedDate.Date).TotalDays / 365, 0);
            Salary.ExpRate = (timeIndex * 0.03 * _employee.SalaryBase) <= maxExpRate ?
               (timeIndex * 0.03 * _employee.SalaryBase) : maxExpRate;
        }
    }
}