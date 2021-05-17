using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Models
{
    public class Characteristic
    {
        public int Id { get; set; }
        [Required]
        public int MotorId { get; set; }
        public Motor Motor { get; set; }
        [Required]
        public int MotorPropertyId { get; set; }
        public MotorProperty MotorProperty { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
