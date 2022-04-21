using FMCore.Interfaces;
using FMCore.Engine;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMCore.Models.Pages;

namespace FMCore.Engine.ContentManagers
{
    internal class FileSystemContentManager : IContentManager<string>
    {
        public FileSystemContentManager(ILogger logger)
        {
            this.logger = logger;
            driver = new FileSystemDriver(logger);
            page = new ContentPage<Dictionary<long, FileSystemInfo>>(logger, 300, 80);
        }


        uint contentLinesOnPage = 30;
        ILogger logger;
        FileSystemDriver driver;
        ContentPage<Dictionary<long, FileSystemInfo>> page;


        long previousIndex = 0;
        int currentIndex = 0;
        Dictionary<long, FileSystemInfo> Content { get; set; }
        Dictionary<long, FileSystemInfo> CuttedContent { get; set; }


        public void PrintPage(string path, long index)
        {
            Content = driver.ListDirectory(path);

            if (CuttedContent is null)
            {
                if (Content.Count <= contentLinesOnPage)
                {
                    CuttedContent = Content;
                    page.Print(CuttedContent); // добавить index
                }
                else
                {
                    for (int i = currentIndex; i < (currentIndex + contentLinesOnPage); i++)
                    {
                        CuttedContent.Add(i, Content.ElementAt(i).Value);
                    }
                }
            }
            else
            {
                if (Content.TryGetValue(index, out var cuttedContentItem))
                {
                    if (CuttedContent.ContainsValue(cuttedContentItem))
                    {
                        page.Print(CuttedContent); // Добавить index
                    }
                    else
                    {
                        if (index > previousIndex)
                        {
                            currentIndex += 1;
                        }
                        if (index < previousIndex)
                        {
                            currentIndex -= 0;
                        }

                        for (int i = currentIndex; i < (currentIndex + contentLinesOnPage); i++)
                        {
                            CuttedContent.Add(i, Content.ElementAt(i).Value);
                        }
                    }
                }
                else
                {
                    logger.Log("Элемента файловой системы с запрашиваемым индексом не существует");
                }
                previousIndex = index;
            }
        }
    }
}
