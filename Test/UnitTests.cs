using FMCore.Interfaces;
using FMCore.Engine.Loggers;
using FMCore.Engine.FileSystem;

using System.Collections.Generic;
using System.IO;
using Xunit;
using FMCore.Engine;

namespace Test
{
    public class UnitTests
    {
        ILogger logger = new ConsoleLogger();
        //ILogger logger = new FileLogger($"{Directory.GetCurrentDirectory()}\\logfile.txt");
        [Fact]
        public void TestFileTreeProvider()
        {
            var FSM = new FileSystemDriver(logger);
            var tree = FSM.ListDirectory(@"D:\1");
            var tree1 = FSM.ListDirectory(null);
            var tree2 = FSM.ListDirectory(" ");
            var tree3 = FSM.ListDirectory("F:\\");
        }
        [Fact]
        public void TestConfigParser()
        {
            ConfigParser parser = new ConfigParser(logger);
            var config = parser.Parse();
        }
    }
}
