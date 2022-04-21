using FMCore.Engine.Loggers;
using FMCore.Interfaces;
using System.Collections.Generic;

using System;
using System.IO;

namespace FMCore.Engine
{
    public class FileSystemDriver
    {
        #region Конструкторы
        public FileSystemDriver(ILogger logger)
        {
            this.logger = logger ?? new ConsoleLogger();
        }
        #endregion

        #region Поля
        private static long _nextId = 0;
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
        public Dictionary<long, FileSystemInfo> ListDirectory(string rootPath)
        {
            if (CheckPath(rootPath))
            {
                try
                {
                    DirectoryInfo root = new DirectoryInfo(rootPath);
                    var tree = new Dictionary<long, FileSystemInfo>();

                    tree.Add(GenerateId(), root);

                    DirectoryInfo[] dirs = root.GetDirectories();
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        var dir = dirs[i];
                        tree.Add(GenerateId(), root);
                    }

                    FileInfo[] files = root.GetFiles();
                    for (int i = 0; i < files.Length; i++)
                    {
                        var file = files[i];
                        tree.Add(GenerateId(), root);
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
        /// Скопировать файл/директорию
        /// </summary>
        /// <param name="src">Путь к файлу/директории, которую копируют</param>
        /// <param name="dst">Путь, куда необходимо скопировать файл/директрию</param>
        public void Copy(string src, string dst)
        {
            if (CheckPath(src) && CheckPath(dst))
            {
                if (IsDirectory(src))
                {
                    CopyDir(src);
                    PasteDir(dst);
                }
                if (IsFile(src))
                {
                    CopyFile(src, dst);
                }
            }
        }

        /// <summary>
        /// Переместить файл/директорию
        /// </summary>
        /// <param name="src">Путь к исходному файлу/директории</param>
        /// <param name="dst">Путь для перемещения</param>
        public void Move(string src, string dst)
        {
            if (CheckPath(src) && CheckPath(dst))
            {
                if (IsDirectory(src))
                {
                    MoveDirectory(src, dst);
                }
                if (IsFile(src))
                {
                    MoveFile(src, dst);
                }
            }
        }

        /// <summary>
        /// Удаление файла/директории
        /// </summary>
        /// <param name="src">Путь к файлу/директории для удаления</param>
        /// <returns>true - в случае успеха и false - в случае ошибки</returns>
        public void Delete(string src)
        {
            try
            {
                if (IsDirectory(src))
                {
                    Directory.Delete(src, true);
                }
                if (IsFile(src))
                {
                    File.Delete(src);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }
        #endregion

        #region Вспомогательные
        /// <summary>
        /// Сгенерировать ID для списка файлов директории
        /// </summary>
        /// <returns>ID</returns>
        private static long GenerateId()
        {
            return _nextId++;
        }

        /// <summary>
        /// Проверяет директорию по указанному пути
        /// </summary>
        /// <param name="fileSystemPath">Путь к элементу, который нужно проверть</param>
        /// <returns>true - указан корректный путь и директория существует, false - одно из условий не выполнено</returns>
        private bool CheckPath(string fileSystemPath)
        {
            if (!string.IsNullOrWhiteSpace(fileSystemPath))
            {
                if (Directory.Exists(fileSystemPath) || File.Exists(fileSystemPath))
                {
                    return true;
                }
                logger.Log($"Ошибка пути: путь {fileSystemPath} не существует");
            }
            else
            {
                logger.Log($"Ошибка аргумента: путь {fileSystemPath} к корню дерева некорректен");
            }
            return false;
        }


        /// <summary>
        /// Копирование файла
        /// </summary>
        /// <param name="src">Файл-источник</param>
        /// <param name="dst">Файл-назначение</param>
        private void CopyFile(string src, string dst)
        {
            try
            {
                File.Copy(src, dst, true);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }

        /// <summary>
        /// Копирование содержимого директории в буфер (свойство <see cref="ContentBuffer"/> класса)
        /// </summary>
        /// <param name="src">Директория для копирования</param>
        private void CopyDir(string src)
        {
            var di = new DirectoryInfo(src);
            try
            {
                ContentBuffer.Enqueue(di);
                DirectoryInfo[] dirs = di.GetDirectories();
                FileInfo[] files = di.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    ContentBuffer.Enqueue(file);
                }

                for (int i = 0; i < dirs.Length; i++)
                {
                    var dir = dirs[i];
                    CopyDir(dir.FullName);
                }
            }
            catch (Exception ex)
            {
                logger.Log($"[{DateTime.Now}] {ex.Message}");
            }
        }


        /// <summary>
        /// Перемещение файла
        /// </summary>
        /// <param name="src">Файл источник</param>
        /// <param name="dst">файл назначения</param>
        /// <returns>true - успех, false - неудача</returns>
        private void MoveFile(string src, string dst)
        {
            try
            {
                File.Move(src, dst, true);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
        }

        /// <summary>
        /// Перемещение директории
        /// </summary>
        /// <param name="src">Директория-источник</param>
        /// <param name="dst">Директория-назначения</param>
        /// <returns>true - успех, false - неудача</returns>
        private void MoveDirectory(string src, string dst)
        {
            CopyDir(src);
            PasteDir(dst);
            Delete(src);
        }


        /// <summary>
        /// Вставка содержимого буфера (свойство <see cref="ContentBuffer"/> класса) в директорию
        /// </summary>
        /// <param name="dst">Директория для вставки</param>
        private void PasteDir(string dst)
        {
            var di = new DirectoryInfo(dst);
            for (int i = 0; i < ContentBuffer.Count; i++)
            {
                var item = ContentBuffer.Dequeue();
                var newFullName = MakeNewPath(item, di);
                try
                {
                    if (IsDirectory(item.FullName))
                    {
                        Directory.CreateDirectory(newFullName);
                    }
                    MoveFile(item.FullName, newFullName);
                }
                catch (Exception ex)
                {
                    logger.Log(ex.Message);
                }
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
        /// Определяет тип элемента файловой системы (директория)
        /// </summary>
        /// <param name="path">Путь к элементу файловой системы</param>
        /// <returns>true - если директория</returns>
        private bool IsDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Определяет тип элемента файловой системы (файл)
        /// </summary>
        /// <param name="path">Элемент файловой системы</param>
        /// <returns>true - если файл</returns>
        private bool IsFile(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
