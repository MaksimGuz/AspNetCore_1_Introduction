using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class CachedImageInfo
    {
        private readonly string _filePath;
        private readonly DateTime _expires;
        private readonly DateTime _expiresForDeleteFile;
        private string _contentType;
        //private readonly Semaphore _semaphore;

        public CachedImageInfo(string filePath, string contentType, ImageCachingOptions options)
        {
            _filePath = filePath;
            _expires = DateTime.UtcNow.AddSeconds(options.CacheExpirationInSeconds);
            _expiresForDeleteFile = _expires.AddSeconds(options.CacheCleanupDelayInSeconds);
            _contentType = contentType;
            //_semaphore = new Semaphore(1, 1, filePath);
        }
        public string FilePath { get { return _filePath; } }
        public DateTime Expires { get { return _expires; } }
        public bool CanBeDeleted() { return _expiresForDeleteFile < DateTime.UtcNow; }
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }
        //public Semaphore Semaphore { get { return _semaphore; } }
    }
}
