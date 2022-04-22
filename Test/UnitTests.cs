using FMCore.Interfaces;
using FMCore.Engine.Loggers;
using Xunit;
using FMCore.Engine;
using FMCore.Engine.ContentManagers;
using FMCore.Models;

namespace Test
{
    public class UnitTests
    {
        ILogger logger = new ConsoleLogger();
        Config config;
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
            config = parser.Parse();
        }
        [Fact]
        public void TestPageManagerPrintPage()
        {
            var pm = new PageManager(logger, new FileSystemDriver(logger), config);
            pm.PrintPage("D:\\1", 0);
        }
    }
}
