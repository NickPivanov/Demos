using System.Collections.Generic;

namespace BlazorHRAgency.Data.Models
{
    public class Salesman : EmployeeBase
    {
        public Salesman()
        {
            Group = EmployeeGroup.Sales;
            SalaryBase = 200.0;
            Subordinates = new();
        }
        public List<EmployeeBase> Subordinates { get; set; }
    }
}
