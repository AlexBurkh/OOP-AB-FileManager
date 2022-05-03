using FMCore.Interfaces;
using System;
using System.IO;

namespace FMCore.Engine.Loggers
{
    public class FileLogger : ILogger
    {
        #region Конструкторы
        public FileLogger(string logFile)
        {
            LogFile = logFile;
        }
        #endregion

        #region Поля

        #endregion

        #region Свойства
        public LoggerType Type { get; } = LoggerType.file;
        public string LogFile { get; private set; }
        #endregion

        #region Методы
        public void Log(string message)
        {
            File.WriteAllText(LogFile, $"[{DateTime.Now}] {message}\n");
        }
        #endregion
    }
}
