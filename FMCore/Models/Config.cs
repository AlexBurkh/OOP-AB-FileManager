using FMCore.Engine.Loggers;

namespace FMCore.Models
{
    public class Config
    {
        public LoggerType LoggerType { get; set; }
        public string LogFile { get; set; }
        public string LastDir { get; set; }
        public ushort ContentLinesOnPage { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
    }
}
