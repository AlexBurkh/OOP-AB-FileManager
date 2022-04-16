using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Models
{
    public class Page
    {
        Dictionary<long, FileSystemInfo> Content { get; set; }

        public void Print()
        {

        }
    }
}
