using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader.Models
{
    [Serializable]
    public class Engine
    {
        public string Name { get; set; }

        public int SerialNumber { get; set; }

        public Operator Operator { get; set; }

        public List<int> Measurements { get; set; }
    }
}
