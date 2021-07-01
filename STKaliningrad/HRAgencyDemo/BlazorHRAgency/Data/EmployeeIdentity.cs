using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHRAgency.Data
{
    public class EmployeeIdentity : IdentityUser
    {
        [Required(ErrorMessage = "Name couldn't be empty")]
        [MaxLength(50, ErrorMessage = "Name length should be less than 50")]
        [MinLength(2, ErrorMessage = "Name should contain at least 2 symbols")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date couldn't be empty")]
        public DateTime EmployedDate { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Group couldn't be empty")]
        public EmployeeGroup Group { get; set; }
    }
}
