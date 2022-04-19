using FMCore.Interfaces;
using System;

namespace FMCore.Engine.ConsoleDrawing
{
    public class ConsoleDrawer
    {
        /// <summary>
        /// Вывести в консоль текст, на опр фоне определенным цветом по координатам смещения
        /// </summary>
        /// <param name="text">Текст для вывода на консоль</param>
        /// <param name="coordinates">Стартовые координаты вывода</param>
        /// <param name="colors">Пара значений цвет фона/цвет текста</param>
        public void DrawColoredAt(string text, (uint x, uint y) coordinates, (ConsoleColor backgroud, ConsoleColor foreground) colors)
        {
            Console.BackgroundColor = colors.backgroud;
            Console.ForegroundColor = colors.foreground;

            DrawAt(text, coordinates);

            Console.ResetColor();
        }

        /// <summary>
        /// Вывести в консоль текст по координатам смещения
        /// </summary>
        /// <param name="text">Текст для вывода на консоль</param>
        /// <param name="coordinates">Стартовые координаты вывода</param>
        public void DrawAt(string text, (uint x, uint y) coordinates)
        {
            Console.SetCursorPosition((int)coordinates.x, (int)coordinates.y);
            Console.Write(text);
        }
    }
}
