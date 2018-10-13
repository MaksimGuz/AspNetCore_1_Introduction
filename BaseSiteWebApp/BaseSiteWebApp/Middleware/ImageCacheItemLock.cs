using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Middleware
{
    public class ImageCacheItemLock : IDisposable
    {
        private static object _listLock = new object();
        private static List<ImageCacheItemLock> _locks = new List<ImageCacheItemLock>();
        private readonly string _itemKey;
        //private object _lock;
        private SemaphoreSlim _semaphore;

        public ImageCacheItemLock(string itemKey)
        {
            lock (_listLock)
            {
                _itemKey = itemKey;
                var existing = _locks.Find(l => l._itemKey.Equals(itemKey));
                _semaphore = existing == null ? new SemaphoreSlim(1, 1) : existing._semaphore;
                _locks.Add(this);
            }
            _semaphore.Wait();
        }

        public string ItemKey { get { return _itemKey; } }

        public void Dispose()
        {
            _semaphore.Release();

            lock (_listLock)
            {
                _locks.Remove(this);
            }
        }
    }
}
