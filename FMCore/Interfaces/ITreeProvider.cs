using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Interfaces
{
    public interface ITreeProvider<T>
        where T : class
    {
        public T MakeTree(string root);
    }
}
