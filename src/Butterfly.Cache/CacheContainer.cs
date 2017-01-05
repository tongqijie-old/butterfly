using Petecat.Caching;
using Petecat.Extending;
using Petecat.Monitor;
using System;
using System.Linq;
using System.IO;

namespace Butterfly.Cache
{
    public class CacheContainer<T> : CacheContainerBase
    {
        private IFileSystemMonitor _FileSystemMonitor;

        public CacheContainer(IFileSystemMonitor fileSystemMonitor, string folder)
        {
            _FileSystemMonitor = fileSystemMonitor;

            Folder = folder;

            Initialize();
        }

        public string Folder { get; protected set; }

        public void Initialize()
        {
            if (!Folder.HasValue())
            {
                throw new Exception(string.Format("folder '{0}' is empty.", Folder));
            }

            if (!Folder.IsFolder())
            {
                Directory.CreateDirectory(Folder);
            }

            foreach (var fileInfo in new DirectoryInfo(Folder).GetFiles("*.json"))
            {
                Add(new JsonFileCacheItem(fileInfo.FullName, fileInfo.FullName, typeof(T)));
            }

            _FileSystemMonitor.Add(this, Folder, OnFileChanged, OnFileCreated, OnFileDeleted, OnFileRenamed);
        }

        public void Uninitialize()
        {
            _FileSystemMonitor.Remove(this, Folder, OnFileChanged, OnFileCreated, OnFileDeleted, OnFileRenamed);
            Items.Keys.ToList().ForEach(x => Remove(x));
        }

        private void OnFileChanged(string path)
        {
            var item = Get(x => x is JsonFileCacheItem && path.EqualsWithPath((x as IFileCacheItem).Path));
            if (item != null)
            {
                item.IsDirty = true;
            }
        }

        private void OnFileCreated(string path)
        {
            Add(new JsonFileCacheItem(path, path, typeof(T)));
        }

        public void OnFileDeleted(string path)
        {
            Remove(path);
        }

        public void OnFileRenamed(string oldPath, string newPath)
        {
            Remove(oldPath);
            Add(new JsonFileCacheItem(newPath, newPath, typeof(T)));
        }

        public T[] Select()
        {
            return Items.Values.Select(x => x.GetValue()).OfType<T>().ToArray();
        }

        public T[] Select(Predicate<T> predicate)
        {
            return Items.Values.Select(x => x.GetValue()).OfType<T>().Where(x => predicate(x)).ToArray();
        }
    }
}
