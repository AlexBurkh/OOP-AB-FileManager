using FMCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace FMCore.Engine.TreeProviders
{
    public class DictProvider : ITreeProvider<Dictionary<long, FileSystemInfo>>
    {
        #region Поля
        ILogger logger;

        private static long _nextId = 0;
        private Dictionary<long, FileSystemInfo> _tree;
        #endregion Поля

        #region Методы
        #region Конструкторы
        public DictProvider(ILogger logger)
        {
            this.logger = logger;
            _tree = new Dictionary<long, FileSystemInfo>();
        }
        #endregion Конструкторы
        #region Static
        private static long GenerateId()
        {
            return _nextId++;
        }
        #endregion Static
        #region Главные
        public Dictionary<long, FileSystemInfo> MakeTree(string root)
        {
            if (!string.IsNullOrWhiteSpace(root))
            {
                if (Directory.Exists(root))
                {
                    try
                    {
                        WalkDirectory(new DirectoryInfo(root));
                        return _tree;
                    }
                    catch (Exception ex)
                    {
                        logger.Log(ex.Message);
                    }
                }
                logger.Log($"Каталог {root} не существует");
            }
            logger.Log($"Ошибка аргумента: путь к корню дерева некорректен");
            return null;
        }
        #endregion Главные
        #region Вспомогательные
        private void WalkDirectory(DirectoryInfo root)
        {
            if (root is null || _tree is null)
            {
                return;
            }
            _tree.Add(GenerateId(), root);
            FileInfo[] files = root.GetFiles();
            DirectoryInfo[] dirs = root.GetDirectories();
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                _tree.Add(GenerateId(), file);
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                var dir = dirs[i];
                WalkDirectory(dir);
            }
        }
        #endregion Вспомогательные
        #endregion Методы
    }
}
