using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public abstract class EmployeeBase
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? LeadId { get; set; }
        [Required]
        public DateTime EmployedDate { get; set; }
        [Required]
        public EmployeeGroup Group { get; set; }
        public double SalaryBase { get; set; }
    }
}
