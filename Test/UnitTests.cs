using FMCore.Interfaces;
using FMCore.Engine.Loggers;
using FMCore.Engine.FileSystem;

using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Test
{
    public class UnitTests
    {
        //ILogger logger = new ConsoleLogger();
        ILogger logger = new FileLogger($"{Directory.GetCurrentDirectory()}\\logfile.txt");
        [Fact]
        public void TestFileTreeProvider()
        {
            var FSM = new FileSystemManager(logger);
            var tree = treeProvider.MakeTree(@"D:\1");
            var tree1 = treeProvider.MakeTree(null);
            var tree2 = treeProvider.MakeTree(" ");
            var tree3 = treeProvider.MakeTree("F:\\");
        }
    }
}
