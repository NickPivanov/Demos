using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Data.Models
{
    public class MotorModel
    {
        [MaxLength(50, ErrorMessage ="Name length should be less than 50")]
        [MinLength(1, ErrorMessage = "Name should contain at least 1 symbol")]
        [Required(ErrorMessage ="Name couldn't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Type couldn't be empty")]
        public MotorType Type { get; set; }
    }
}
