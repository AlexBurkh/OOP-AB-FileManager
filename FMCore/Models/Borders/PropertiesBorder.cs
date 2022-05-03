using FMCore.Engine.ConsoleDrawing;
using FMCore.Engine.ContentManagers;
using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Models.Borders
{
    internal sealed class PropertiesBorder : Border
    {
        #region Конструкторы
        internal PropertiesBorder(ILogger logger, ConsoleDrawer drawer,uint height, uint width)
            : base(logger, drawer, height, width)
        {
            StartCoordinates = (0, PageManager.CONTENT_LENGTH + 2);
        }
        #endregion

        #region Методы
        public override void Draw()
        {
            drawer.DrawAt($"{(char)Symbols.CenterLeftCorner}", (StartCoordinates.x, StartCoordinates.y));
            for (uint j = 1; j < (Width - 1); j++)
            {
                drawer.DrawAt($"{(char)Symbols.Horizontal}", (j, StartCoordinates.y));
            }
            drawer.DrawAt($"{(char)Symbols.CenterRightCorner}", (Width - 1, StartCoordinates.y));
            drawer.DrawAt($"\tИмя:     ", (0, StartCoordinates.y + 1));
            drawer.DrawAt($"\tСоздан:  ", (0, StartCoordinates.y + 2));
            drawer.DrawAt($"\tИзменен: ", (0, StartCoordinates.y + 3));
            drawer.DrawAt($"\tОткрыт:  ", (0, StartCoordinates.y + 4));
            drawer.DrawAt($"\tРазмер:  ", (0, StartCoordinates.y + 5));
        }
        #endregion
    }
}
