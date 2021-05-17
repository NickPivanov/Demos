using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoConsoleReader.TextFileReaders
{
    public class TxtReader : IFileReader
    {
        public void CreateFile(string path, object[] data)
        {
            using (StreamWriter sw = new StreamWriter(path, append: false))
            {
                foreach (string s in data)
                    sw.WriteLine(s);
            }
        }

        public void AddDataToFile(string path, object data)
        {
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLine(data as string);
            }
        }

        public IEnumerable<object> ReadFile<T>(string path)
        {
            List<object> result = new();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    result.Add(line);
            }

            return result;
        }
    }
}
