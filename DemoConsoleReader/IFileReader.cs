using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader
{
    public interface IFileReader
    {
        public void CreateFile(string path, object[] data);
        public IEnumerable<object> ReadFile<T>(string path);
        public void AddDataToFile(string path, object data);
    }
}
