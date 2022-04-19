using FMCore.Engine.ConsoleDrawing;
using FMCore.Engine.FileSystem;
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
    public class ContentPage : Page
    {
        #region Конструкторы
        public ContentPage(IContentManager<Dictionary<long, FileSystemInfo>, string> contentManager,

                           ILogger logger, 
                           uint height, 
                           uint width)
            : base(logger, height, width)
        {
            _contentManager = contentManager;
            _borders = new Border[]
            {
                new MenuBorder(logger, height, width),
                new ContentBorder(logger, height, width),
                new PropertiesBorder(logger, height, width)
            };
        }
        #endregion


        #region Поля
        ConsoleDrawer drawer = new ConsoleDrawer();
        IContentManager<Dictionary<long, FileSystemInfo>, string> _contentManager;
        Border[] _borders;
        #endregion


        #region Свойства
        Dictionary<long, FileSystemInfo> Content { get; set; }
        #endregion


        #region Методы

        /// <summary>
        /// Вывод страницы с контентом
        /// </summary>
        public override void Print()
        {
            PrintBorders();
            PrintContent();
        }

        /// <summary>
        /// Загрузить контент
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(string dir)
        {
            if (dir is not null)
            {
                Content = _contentManager.LoadContent(dir);
            }
        }

        private void PrintBorders()
        {
            for (int i = 0; i < _borders.Length; i++)
            {
                var border = _borders[i];
                border.Draw();
            }
        }
        private void PrintContent()
        {
            for (int i = 0; i < PagedContent.Count; i++)
            {
                if (PagedContent.TryGetValue(i, out var content))
                {
                    if (content is DirectoryInfo)
                    {
                        drawer.DrawColoredAt(content.FullName, (2, 2), (ConsoleColor.Black, ConsoleColor.DarkYellow));
                    }
                    if (content is FileInfo)
                    {
                        drawer.DrawColoredAt(content.Name, (2, 2), (ConsoleColor.Black, ConsoleColor.Gray));
                    }
                }
            }
        }
        private void ChooseContentByIndex(long startIndex)
        {

        }
        #endregion
    }
}
