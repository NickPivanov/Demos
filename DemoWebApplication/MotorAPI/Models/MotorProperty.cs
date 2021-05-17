using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MotorAPI.Models
{
    public class MotorProperty
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public MotorType? MotorType { get; set; }
    }
}
