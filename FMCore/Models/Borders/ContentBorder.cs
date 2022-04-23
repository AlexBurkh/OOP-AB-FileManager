using FMCore.Engine.ConsoleDrawing;
using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMCore.Models.Borders
{
    internal sealed class ContentBorder : Border
    {
        #region Конструкторы
        internal ContentBorder(ILogger logger, ConsoleDrawer drawer, uint height, uint width)
            : base(logger, drawer, height, width)
        {
            StartCoordinates = (0, 3);
        }
        #endregion

        #region Методы
        public override void Draw()
        {
            for (uint i = StartCoordinates.y; i < Height; i++)
            {
                if ((i >= StartCoordinates.y) && (i < (Height - 2)))
                {
                    drawer.DrawAt($"{(char)Symbols.Vertical}", (StartCoordinates.x, i));
                    drawer.DrawAt($"{(char)Symbols.Vertical}", (Width - 1, i));
                }

                if (i == (Height - 2))
                {
                    drawer.DrawAt($"{(char)Symbols.BottomLeftCorner}", (StartCoordinates.x, i));
                    for (uint j = 1; j < (Width - 1); j++)
                    {
                        drawer.DrawAt($"{(char)Symbols.Horizontal}", (j, i));
                    }
                    drawer.DrawAt($"{(char)Symbols.BottomRightCorner}", (Width - 1, i));
                }
            }
        }
        #endregion
    }
}

/*
public CommonBorder(int height, int width)
               : base(height, width) { }

/// <summary>
/// Формирование строки, содержащей общую границу окна
/// </summary>
/// <returns></returns>
public override string Draw()
{

}
*/