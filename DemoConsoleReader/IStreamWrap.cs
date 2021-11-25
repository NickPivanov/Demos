using DemoConsoleReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleReader
{
    public interface IStreamWrap
    {
        void WriteLine(string path, string s, bool toExistingFile);
        IEnumerable<string> ReadLine(string path);
        IEnumerable<Engine> ReadLineFromJson(string path);
    }
}
