using System.Collections.Generic;

namespace DemoConsoleReader.TextFileReaders
{
    public class TxtReader : IFileReader
    {
        private readonly IStreamWrap _streamWrap;
        public TxtReader(IStreamWrap streamWrap)
        {
            _streamWrap = streamWrap;
        }

        public void CreateFile(string path, object[] data)
        {
            foreach (string s in data)
                _streamWrap.WriteLine(path, s, false);
        }

        public void AddDataToFile(string path, object data)
        {
            _streamWrap.WriteLine(path, data as string, true);
        }

        public IEnumerable<object> ReadFile<T>(string path)
        {
            return _streamWrap.ReadLine(path);
        }
    }
}
