using DemoConsoleReader.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoConsoleReader.TextFileReaders
{
    public class StreamWrapperJson : IStreamWrap
    {
        public IEnumerable<Engine> ReadLineFromJson(string path)
        {
            List<Engine> result = new();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                    result.Add(JsonConvert.DeserializeObject<Engine>(line));
                return result;
            }
        }

        public void WriteLine(string path, string s, bool toExistingFile)
        {
            if(toExistingFile)
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write($"{s}\n");
                }
            else
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write($"{s}\n");
                }
        }

        public IEnumerable<string> ReadLine(string path)
        {
            throw new System.NotImplementedException();
        }

    }
}
