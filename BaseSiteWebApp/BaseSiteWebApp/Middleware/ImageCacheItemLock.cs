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
        private object _lock;

        public ImageCacheItemLock(string itemKey)
        {
            lock (_listLock)
            {
                _itemKey = itemKey;
                var existing = _locks.Find(l => l._itemKey.Equals(itemKey));
                _lock = existing == null ? new object() : existing._lock;
                _locks.Add(this);
            }
            Monitor.Enter(_lock);
        }

        public string ItemKey { get { return _itemKey; } }

        public void Dispose()
        {
            if (Monitor.IsEntered(_lock))
                Monitor.Exit(_lock);

            lock (_listLock)
            {
                _locks.Remove(this);
            }
        }
    }
}
