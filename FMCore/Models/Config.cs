using FMCore.Engine.Loggers;

namespace FMCore.Models
{
    public class Config
    {
        public string LastDir { get; set; }
        public LoggerType LoggerType { get; set; }
        public string LogFile { get; set; }
    }
}
