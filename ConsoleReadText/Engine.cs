using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleReadText
{
    //class for serialization to json file
    public class Engine
    {
        public string Name { get; set; }

        public int SerialNumber { get; set; }

        public User user { get; set; }

        public List<int> Measures { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
    }
}
