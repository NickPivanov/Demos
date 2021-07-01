using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
