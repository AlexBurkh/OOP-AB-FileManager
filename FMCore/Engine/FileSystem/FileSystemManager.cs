using FMCore.Engine.Loggers;
using FMCore.Interfaces;
using System.Collections.Generic;

using System;
using System.IO;

namespace FMCore.Engine.FileSystem
{
    public class FileSystemManager
    {
        #region Конструкторы
        public FileSystemManager(ILogger logger)
        {
            this.logger = logger ?? new ConsoleLogger();
        }
        #endregion

        #region Поля
        private ILogger logger;
        #endregion

        #region Свойства
        /// <summary>
        /// Буфер для хранения содержимого директории для последующей вставки
        /// </summary>
        private Queue<FileSystemInfo> ContentBuffer { get; set; }
        #endregion

        #region Методы
        #region Главные
        /// <summary>
        /// Перечислить содержимое директории
        /// </summary>
        /// <param name="rootPath">Путь к директории</param>
        /// <returns>Список элементов ФС</returns>
        public List<FileSystemInfo> ListDirectory(string rootPath)
        {
            if (CheckDirectoryPath(rootPath))
            {
                try
                {
                    DirectoryInfo root = new DirectoryInfo(rootPath);
                    var tree = new List<FileSystemInfo>();

                    tree.Add(root);

                    DirectoryInfo[] dirs = root.GetDirectories();
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        var dir = dirs[i];
                        tree.Add(dir);
                    }

                    FileInfo[] files = root.GetFiles();
                    for (int i = 0; i < files.Length; i++)
                    {
                        var file = files[i];
                        tree.Add(file);
                    }

                    return tree;
                }
                catch (Exception ex)
                {
                    logger.Log(ex.Message);
                }
            }
            return null;
        }
        
        /// <summary>
        /// Скопировать директорию
        /// </summary>
        /// <param name="sourcePath">Путь к директории, которую копируют</param>
        /// <param name="destPath">Путь, куда необходимо скопировать директрию</param>
        public void CopyDirectory(string sourcePath, string destPath)
        {
            if (CheckDirectoryPath(sourcePath) && CheckDirectoryPath(destPath))
            {
                CopyDir(new DirectoryInfo(sourcePath));
                PasteDir(new DirectoryInfo(destPath));
            }
        }

        /// <summary>
        /// Переместить директорию
        /// </summary>
        /// <param name="sourcePath">Путь старой директории</param>
        /// <param name="destPath">Путь для перемещения</param>
        public void MoveDirectory(string sourcePath, string destPath)
        {
            if (CheckDirectoryPath(sourcePath) && CheckDirectoryPath(destPath))
            {
                CopyDir(new DirectoryInfo(sourcePath));
                PasteDir(new DirectoryInfo(destPath));
                try
                {
                    Directory.Delete(sourcePath, true);
                }
                catch (Exception ex)
                {
                    logger.Log(ex.Message);
                }
            }
        }
        #endregion
        #region Вспомогательные
        /// <summary>
        /// Копирование содержимого директории в буфер (свойство <see cref="ContentBuffer"/> класса)
        /// </summary>
        /// <param name="src">Директория для копирования</param>
        private void CopyDir(DirectoryInfo src)
        {
            ContentBuffer.Enqueue(src);
            DirectoryInfo[] dirs = src.GetDirectories();
            FileInfo[] files = src.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                ContentBuffer.Enqueue(file);
            }

            for (int i = 0; i < dirs.Length; i++)
            {
                var dir = dirs[i];
                CopyDir(dir);
            }
        }

        /// <summary>
        /// Вставка содержимого буфера (свойство <see cref="ContentBuffer"/> класса) в директорию
        /// </summary>
        /// <param name="dst">Директория для вставки</param>
        private void PasteDir(DirectoryInfo dst)
        {
            for (int i = 0; i < ContentBuffer.Count; i++)
            {
                var item = ContentBuffer.Dequeue();
                var newFullName = MakeNewPath(item, dst);
                if (IsDirectory(item))
                {
                    Directory.CreateDirectory(newFullName);
                }
                File.Create(newFullName);
            }
        }

        /// <summary>
        /// Подготавливает новое имя для файла/директории
        /// </summary>
        /// <param name="oldItem">Старый элемент с полным именем</param>
        /// <param name="newItem">Путь к новой директории</param>
        /// <returns></returns>
        private string MakeNewPath(FileSystemInfo oldItem, DirectoryInfo newItem)
        {
            return $"{newItem.FullName}\\{oldItem.FullName.Substring(oldItem.Name.Length)}";

        }
        /// <summary>
        /// Проверяет директорию по указанному пути
        /// </summary>
        /// <param name="fileSystemPath">Путь к элементу, который нужно проверть</param>
        /// <returns>true - указан корректный путь и директория существует, false - одно из условий не выполнено</returns>
        private bool CheckDirectoryPath(string fileSystemPath)
        {
            if (!string.IsNullOrWhiteSpace(fileSystemPath))
            {
                if (IsDirectory(fileSystemPath))
                {
                    return true;
                }
                logger.Log($"Каталог {fileSystemPath} не существует");
            }
            logger.Log($"Ошибка аргумента: путь {fileSystemPath} к корню дерева некорректен");
            return false;
        }

        /// <summary>
        /// Определяет тип элемента файловой системы (файл или директория)
        /// </summary>
        /// <param name="path">Путь к элементу файловой системы</param>
        /// <returns>true - если директория, false - если файл</returns>
        private bool IsDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Определяет тип элемента файловой системы (файл или директория)
        /// </summary>
        /// <param name="path">Элемент файловой системы</param>
        /// <returns>true - если директория, false - если файл</returns>
        private bool IsDirectory(FileSystemInfo path)
        {
            if (Directory.Exists(path.FullName))
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
