using FMCore.Engine.ConsoleDrawing;
using FMCore.Interfaces;
using FMCore.Models.Borders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FMCore.Models.Pages
{
    public class ContentPage<T> : Page<T>
        where T : ICollection<FileSystemInfo>
    {
        #region Конструкторы
        public ContentPage(ILogger logger,
                           ConsoleDrawer drawer,
                           uint height, 
                           uint width)
            : base(logger, height, width)
        {
            this.drawer = drawer;
            _borders = new Border[]
            {
                new MenuBorder(logger, drawer, 3, Width),
                new ContentBorder(logger, drawer, Height, Width),
                new PropertiesBorder(logger, drawer, 10, Width)
            };
        }
        #endregion


        #region Поля
        ConsoleDrawer drawer;
        Border[] _borders;
        (uint x, uint y) startCoordinates = (2, 4);
        #endregion


        #region Свойства
        List<FileSystemInfo> Content { get; set; }
        #endregion


        #region Методы

        /// <summary>
        /// Вывод страницы с контентом
        /// </summary>
        public override void Print(T content, int coloredItemIndex)
        {
            PrintBorders();
            PrintContent(content, coloredItemIndex);
        }

        private void PrintBorders()
        {
            for (int i = 0; i < _borders.Length; i++)
            {
                var border = _borders[i];
                border.Draw();
            }
        }
        private void PrintContent(T content, int coloredItemIndex)
        {
            Content = content as List<FileSystemInfo>;
            for (uint i = 0; i < content.Count; i++)
            {
                var contentItem = (content as List<FileSystemInfo>)[(int) i];
                var background = ConsoleColor.Black;
                if (i == coloredItemIndex)
                {
                    background = ConsoleColor.Cyan;
                }
                if (contentItem is DirectoryInfo)
                {
                    drawer.DrawColoredAt(contentItem.FullName, (startCoordinates.x, startCoordinates.y + i), (background, ConsoleColor.DarkYellow));
                }
                if (contentItem is FileInfo)
                {
                    drawer.DrawColoredAt(contentItem.Name, (startCoordinates.x, startCoordinates.y + i), (background, ConsoleColor.Gray));
                }
            }
        }
        #endregion
    }
}
