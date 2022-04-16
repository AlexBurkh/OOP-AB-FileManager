using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Models.Borders
{
    internal sealed class MenuBorder : Border
    {
        #region Конструкторы
        internal MenuBorder(ILogger logger, uint height, uint width)
            : base(logger, height, width)
        {

        }
        #endregion


        #region Методы
        public override void Draw()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
