using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Engine.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public LoggerType Type { get; } = LoggerType.console;
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}
