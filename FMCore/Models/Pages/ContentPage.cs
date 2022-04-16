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
        public ContentPage(ILogger logger, uint height, uint width)
            : base(logger, height, width)
        {

        }
        #endregion


        #region Свойства
        Border[] Borders { get; set; }
        Dictionary<long, FileSystemInfo> Content { get; set; }
        #endregion


        #region Методы
        public override void Print()
        {
            PrintBorders();
            PrintContent();
        }
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
