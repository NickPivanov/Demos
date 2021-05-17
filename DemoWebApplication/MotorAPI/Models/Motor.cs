using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MotorAPI.Models
{
    public class Motor
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        public MotorType Type { get; set; }
        public IEnumerable<Characteristic> Characteristics { get; set; }
        public IEnumerable<Measurement> MeasurementsLog { get; set; }
    }
}
