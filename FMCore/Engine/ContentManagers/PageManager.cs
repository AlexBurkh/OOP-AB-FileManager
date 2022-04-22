using FMCore.Interfaces;
using System.Collections.Generic;
using System.IO;
using FMCore.Models.Pages;
using FMCore.Models;

namespace FMCore.Engine.ContentManagers
{
    public class PageManager
    {
        public PageManager(ILogger logger, FileSystemDriver driver, Config config)
        {
            this.logger = logger;
            this.driver = driver;
            this.appConfig = config;
            driver = new FileSystemDriver(logger);
            CONTENT_LENGTH = appConfig?.ContentLinesOnPage ?? 30;
            page = new ContentPage<List<FileSystemInfo>>(logger, (uint) appConfig.WindowHeight, (uint) appConfig.WindowWidth);
        }

        ILogger logger;
        FileSystemDriver driver;
        Config appConfig;
        Page<List<FileSystemInfo>> page;

        static ushort CONTENT_LENGTH;
        int startContentIndex = 0;
        int endContentIndex = (int) CONTENT_LENGTH - 1;


        public int CurrSelectedIndex = 0;
        public int PrevSelectedIndex = 0;
        public string WorkDir { get; private set; }
        public string PrevWorkDir { get; private set; }
        public int MaxIndex { get; private set; }
        public FileSystemInfo SelectedItem { get; private set; }
        List<FileSystemInfo> Content { get; set; }
        List<FileSystemInfo> CuttedContent { get; set; }


        public void PrintPage(string workDir, int itemIndex)
        {
            if (workDir != PrevWorkDir || Content is null)
            {
                Content = driver.ListDirectory(workDir);
                MaxIndex = Content.Count - 1;
            }

            if (CuttedContent is null)
            {
                if (Content.Count > CONTENT_LENGTH)
                {
                    CuttedContent = Content.GetRange(0, CONTENT_LENGTH);
                }
                if (Content.Count <= CONTENT_LENGTH)
                {
                    CuttedContent = Content.GetRange(0, Content.Count);
                }
            }

            if (CuttedContent.Contains(Content[itemIndex]))
            {
                for (int i = 0; i < CuttedContent.Count; i++)
                {
                    if (CuttedContent[i] == Content[itemIndex])
                    {
                        SelectedItem = CuttedContent[i];
                        break;
                    }
                }
                if (itemIndex > CurrSelectedIndex)
                {
                    CurrSelectedIndex += 1;
                }
                else
                {
                    if (CurrSelectedIndex > 0)
                    {
                        CurrSelectedIndex -= 1;
                    }
                }
            }
            else
            {
                if (itemIndex > PrevSelectedIndex)
                {
                    startContentIndex += 1;
                    endContentIndex += 1;
                    CurrSelectedIndex += 1;
                }
                if (itemIndex < PrevSelectedIndex)
                {
                    if (startContentIndex > 0 && endContentIndex > 0 && CurrSelectedIndex > 0)
                    {
                        startContentIndex -= 1;
                        endContentIndex -= 1;
                        CurrSelectedIndex -= 1;
                    }
                }
            }

            if (Content.Count > CONTENT_LENGTH)
            {
                CuttedContent = Content.GetRange(startContentIndex, CONTENT_LENGTH);
            }
            if (Content.Count <= CONTENT_LENGTH)
            {
                CuttedContent = Content.GetRange(startContentIndex, Content.Count);
            }

            page.Print(CuttedContent, CurrSelectedIndex);
        }
    }
}
