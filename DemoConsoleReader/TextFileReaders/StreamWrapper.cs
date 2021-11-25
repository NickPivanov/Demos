using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader.TextFileReaders
{
    public class StreamWrapper : IStreamWrap
    {
        public IEnumerable<string> ReadLine(string path)
        {
            List<string> result = new();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                    result.Add(line);
                return result;
            }
        }

        public void WriteLine(string path, string s, bool append)
        {
            using (StreamWriter sw = new StreamWriter(path, append))
            {
                sw.WriteLine(s);
            }
        }
    }
}
