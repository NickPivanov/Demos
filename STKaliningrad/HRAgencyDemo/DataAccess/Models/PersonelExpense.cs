using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PersonelExpense
    {
        public int Id { get; set; }
        public EmployeeBase Employee { get; set; }
        public double CurrentSalary { get; set; }
    }
}
