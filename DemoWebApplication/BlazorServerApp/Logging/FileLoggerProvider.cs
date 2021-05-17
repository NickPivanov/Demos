using Microsoft.Extensions.Logging;

namespace BlazorServerApp.Logging
{
    internal class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        public FileLoggerProvider(string Path)
        {
            path = Path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TxtFileLogger(path);
        }

        public void Dispose()
        {
        }
    }
}