using FMCore.Interfaces;
using FMCore.Engine.Loggers;
using FMCore.Engine.TreeProviders;

using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Test
{
    public class UnitTests
    {
        ILogger logger = new ConsoleLogger();
        [Fact]
        public void TestFileTreeProvider()
        {
            ITreeProvider<Dictionary<long, FileSystemInfo>> treeProvider = new DictProvider(logger);
            var tree = treeProvider.MakeTree(@"D:\0");
            var tree1 = treeProvider.MakeTree(null);
            var tree2 = treeProvider.MakeTree(" ");
            var tree3 = treeProvider.MakeTree("F:\\");
        }
    }
}
