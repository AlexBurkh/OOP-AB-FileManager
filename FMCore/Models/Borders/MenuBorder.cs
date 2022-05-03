using FMCore.Engine.ConsoleDrawing;
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
        internal MenuBorder(ILogger logger, ConsoleDrawer drawer, uint height, uint width)
            : base(logger, drawer, height, width)
        {
            StartCoordinates = (0, 0);
        }
        #endregion


        #region Методы
        public override void Draw()
        {
            for (uint i = StartCoordinates.y; i <= Height; i++)
            {
                if (i == 0)
                {
                    drawer.DrawAt($"{(char)Symbols.TopLeftCorner}", (StartCoordinates.x, i));
                    for (uint j = 1; j < (Width - 1); j++)
                    {
                        if (j == 18 || j == 36 || j == 54)
                        {
                            drawer.DrawAt($"{(char)Symbols.TopCenter}", (j, i));
                            continue;
                        }
                        drawer.DrawAt($"{(char)Symbols.Horizontal}", (j, i));
                    }
                    drawer.DrawAt($"{(char)Symbols.TopRightCorner}", (Width - 1, i));
                }

                if (i == 1)
                {
                    drawer.DrawAt($"{(char)Symbols.Vertical}", (StartCoordinates.x, i));
                    for (uint j = 1; j < 4; j++)
                    {
                        drawer.DrawAt($"{(char)Symbols.Vertical}", (j*18, i));
                    }
                    drawer.DrawAt($"{(char)Symbols.Vertical}", (Width - 1, i));
                }

                if (i == 2)
                {
                    drawer.DrawAt($"{(char)Symbols.CenterLeftCorner}", (StartCoordinates.x, i));
                    for (uint j = 1; j < (Width - 1); j++)
                    {
                        if (j == 18 || j == 36 || j == 54)
                        {
                            drawer.DrawAt($"{(char)Symbols.BottomCenter}", (j, i));
                            continue;
                        }
                        drawer.DrawAt($"{(char)Symbols.Horizontal}", (j, i));
                    }
                    drawer.DrawAt($"{(char)Symbols.CenterRightCorner}", (Width - 1, i));
                }
            }
        }
        #endregion
    }
}
