using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader
{
    public interface IStreamWrap
    {
        void WriteLine(string path, string s, bool append);
        IEnumerable<string> ReadLine(string path);
    }
}
