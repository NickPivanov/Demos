using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHRAgency.Data
{
    public class EmployeeModel
    {
        [Required(ErrorMessage ="Name couldn't be empty")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [MinLength(2, ErrorMessage = "Name should contain at least 2 symbols")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date couldn't be empty")]
        public DateTime EmployedDate { get; set; }
        [Required(ErrorMessage = "Group couldn't be empty")]
        public EmployeeGroup Group { get; set; }
    }
}
