using System.Collections.Generic;

namespace BlazorHRAgency.Data.Models
{
    public class Manager : EmployeeBase
    {
        public Manager()
        {
            Group = EmployeeGroup.Management;
            SalaryBase = 150.0;
            Subordinates = new();
        }
        public List<EmployeeBase> Subordinates { get; set; }
    }
}
