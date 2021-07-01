using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAgencyAPI
{
    public class EmployeeFactory
    {
        private readonly EmployeeGroup _employeeGroup;

        public EmployeeFactory(EmployeeGroup employeeGroup)
        {
            _employeeGroup = employeeGroup;
        }

        public EmployeeBase CreateEmployee(string name, DateTime employedDate)
        {
            EmployeeBase employee = _employeeGroup switch
            {
                EmployeeGroup.Employees => new Employee() {Name = name, EmployedDate = employedDate },
                EmployeeGroup.Management => new Manager() { Name = name, EmployedDate = employedDate },
                EmployeeGroup.Sales => new Salesman() { Name = name, EmployedDate = employedDate },
                _ => throw new Exception("Group doesn't exist")
            };

            return employee;
        }
    }
}
