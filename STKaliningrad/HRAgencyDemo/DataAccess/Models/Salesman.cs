using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
