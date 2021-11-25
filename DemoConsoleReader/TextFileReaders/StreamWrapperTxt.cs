using DemoConsoleReader.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoConsoleReader.TextFileReaders
{
    public class StreamWrapperTxt : IStreamWrap
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

        public void WriteLine(string path, string s, bool toExistingFile)
        {
            if (toExistingFile)
                using (StreamWriter sw = new StreamWriter(path, append: true))
                {
                    sw.WriteLine(s);
                }
            else
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(s);
                }
        }

        public IEnumerable<Engine> ReadLineFromJson(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
