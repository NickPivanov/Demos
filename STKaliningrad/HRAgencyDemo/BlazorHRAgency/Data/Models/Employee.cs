namespace BlazorHRAgency.Data.Models
{
    public class Employee : EmployeeBase
    {
        public Employee()
        {
            Group = EmployeeGroup.Employees;
            SalaryBase = 100.0;
        }
    }
}
