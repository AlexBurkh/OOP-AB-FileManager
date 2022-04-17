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
        public ContentPage(ILogger logger, FileSystemManager contentManager, uint height, uint width)
            : base(logger, height, width)
        {
            _contentManager = contentManager;
        }
        #endregion


        #region Поля
        FileSystemManager _contentManager;
        #endregion


        #region Свойства
        Border[] Borders { get; set; }
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
        public void UploadContent(Dictionary<long, FileSystemInfo> content)
        {
            if (content is null) { return; }
            Content = content;
        }

        private void PrintBorders()
        {
            for (int i = 0; i < Borders.Length; i++)
            {
                var border = Borders[i];
                border.Draw();
            }
        }
        private void PrintContent()
        {
            for (int i = 0; i < Content.Count; i++)
            {
                if (Content.TryGetValue(i, out var content))
                {
                    /// printing "ContentString"
                }
            }
        }
        #endregion
    }
}
