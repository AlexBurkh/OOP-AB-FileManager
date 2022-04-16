using FMCore.Interfaces;
using System;

namespace FMCore.Engine.ConsoleDrawing
{
    public class ConsoleDrawer
    {
        ConsoleDrawer(ILogger logger)
        {
            this.logger = logger;
        }

        ILogger logger;


        public void DrawColoredAt(string text, (uint x, uint y) coordinates, (ConsoleColor backgroud, ConsoleColor foreground) colors)
        {
            Console.BackgroundColor = colors.backgroud;
            Console.ForegroundColor = colors.foreground;

            DrawAt(text, coordinates);

            Console.ResetColor();
        }

        public void DrawAt(string text, (uint x, uint y) coordinates)
        {
            Console.SetCursorPosition((int)coordinates.x, (int)coordinates.y);
            Console.Write(text);
        }
    }
}
