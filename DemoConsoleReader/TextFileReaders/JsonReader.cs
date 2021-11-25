using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoConsoleReader.TextFileReaders
{
    public class JsonReader : IFileReader
    {
        private readonly IStreamWrap _streamWrap;

        public JsonReader(IStreamWrap streamWrap)
        {
            _streamWrap = streamWrap;
        }

        public void CreateFile(string path, object[] data)
        {
             foreach (var item in data)
             {
                 var engineToJson = JsonConvert.SerializeObject(item);
                 _streamWrap.WriteLine(path, engineToJson, false);
             }
        }

        public void AddDataToFile(string path, object data)
        {
            var engineToJson = JsonConvert.SerializeObject(data);
            _streamWrap.WriteLine(path, engineToJson, true);
        }

        public IEnumerable<object> ReadFile<T>(string path)
        {
            return _streamWrap.ReadLineFromJson(path);
        }
    }
}
