using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Logging
{
    public class TxtFileLogger : ILogger
    {
        private string filepath;
        private static object _lock = new();
        
        public TxtFileLogger(string FilePath)
        {
            filepath = FilePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is not null)
            {
                lock (_lock)
                    File.AppendAllText(filepath, $"{DateTime.Now} {logLevel}: {formatter(state, exception)}{Environment.NewLine}");
            }
        }
    }
}
