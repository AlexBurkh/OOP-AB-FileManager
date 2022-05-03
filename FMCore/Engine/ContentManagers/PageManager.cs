using FMCore.Interfaces;
using System.Collections.Generic;
using System.IO;
using FMCore.Models.Pages;
using FMCore.Models;
using FMCore.Engine.ConsoleDrawing;
using System;

namespace FMCore.Engine.ContentManagers
{
    public class PageManager
    {
        public PageManager(ILogger logger, FileSystemDriver driver, Config config)
        {
            CONTENT_LENGTH = appConfig?.ContentLinesOnPage ?? 30;
            this.logger = logger;
            this.driver = driver;
            this.appConfig = config;
            this.driver = driver;
            drawer = new ConsoleDrawer(appConfig.WindowWidth);
            page = new ContentPage<List<FileSystemInfo>>(logger, drawer, (uint) appConfig.WindowHeight, (uint) appConfig.WindowWidth);
        }

        ILogger logger;
        FileSystemDriver driver;
        ConsoleDrawer drawer;
        Config appConfig;
        Page<List<FileSystemInfo>> page;

        public static uint CONTENT_LENGTH;
        int startContentIndex = 0;
        int endContentIndex = (int) CONTENT_LENGTH - 1;


        public int CurrSelectedIndex = 0;
        public int PrevSelectedIndex = 0;
        public string WorkDir { get; private set; }
        public string PrevWorkDir { get; private set; } = string.Empty;
        public int MaxIndex { get; private set; }
        public FileSystemInfo SelectedItem { get; private set; }
        List<FileSystemInfo> Content { get; set; }
        List<FileSystemInfo> CuttedContent { get; set; }


        public void PrintPage(string workDir, int itemIndex)
        {
            Console.Clear();
            WorkDir = workDir;
            /*            if (WorkDir != PrevWorkDir || Content is null)
                        {
                            Content = driver.ListDirectory(WorkDir);
                            MaxIndex = Content.Count - 1;
                            CurrSelectedIndex = itemIndex;
                        }*/
            Content = driver.ListDirectory(WorkDir);
            MaxIndex = Content.Count - 1;
            CurrSelectedIndex = itemIndex;

            if (CuttedContent is null || (CuttedContent[0] != Content[0]))
            {
                if (Content.Count > CONTENT_LENGTH)
                {
                    CuttedContent = Content.GetRange(0, (int) CONTENT_LENGTH);
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
                if (itemIndex < CurrSelectedIndex)
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
                CuttedContent = Content.GetRange(startContentIndex, (int) CONTENT_LENGTH);
            }
            if (Content.Count <= CONTENT_LENGTH)
            {
                CuttedContent = Content.GetRange(startContentIndex, Content.Count - startContentIndex);
            }
            PrevWorkDir = WorkDir;
            page.Print(CuttedContent, CurrSelectedIndex);
        }
    }
}
