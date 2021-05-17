using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader.TextFileReaders
{
    public class JsonReader : IFileReader
    {
        public void CreateFile(string path, object[] data)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in data)
                {
                    var engineToJson = JsonConvert.SerializeObject(item);
                    sw.Write($"{engineToJson}\n");
                }
            }
        }

        public void AddDataToFile(string path, object data)
        {
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                 var engineToJson = JsonConvert.SerializeObject(data);
                 sw.Write($"{engineToJson}\n");
            }
        }

        public IEnumerable<object> ReadFile<T>(string path)
        {
            List<object> result = new(); 
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    result.Add(JsonConvert.DeserializeObject<T>(line));
            }

            return result;
        }
    }
}
