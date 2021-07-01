using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDepartment
{
    public class Salary
    {
        public double SalaryBase { get; set; }
        public double ExpRate { get; set; }
        public double ManagmentRate { get; set; }
        public double CalculateSalary()
        {
            return Math.Round(SalaryBase + ExpRate + ManagmentRate, 2);
        }
    }
}
