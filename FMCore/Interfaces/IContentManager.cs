﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Interfaces
{
    public interface IContentManager<TResult, KParam>
    {
        public TResult LoadContent(KParam);
    }
}
