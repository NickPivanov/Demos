using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorAPI.Models
{
    public class Measurement
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public int MotorId { get; set; }
        public Motor Motor { get; set; }

        public int MotorPropertyId { get; set; }
        public MotorProperty MotorProperty { get; set; }
        public double Value { get; set; }
    }
}
