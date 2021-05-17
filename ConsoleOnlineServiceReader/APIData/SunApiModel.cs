using System;

namespace ConsoleOnlineServiceReader.APIData
{
    public class SunApiModel
    {
        public Sun Results { get; set; }
    }

    public class Sun
    {
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}
