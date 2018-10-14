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
        private string _requestPath;

        public CachedImageInfo(string filePath, string contentType, ImageCachingOptions options, string requestPath)
        {
            _filePath = filePath;
            _expires = DateTime.UtcNow.AddSeconds(options.CacheExpirationInSeconds);
            _expiresForDeleteFile = _expires.AddSeconds(options.CacheCleanupDelayInSeconds);
            _contentType = contentType;
            _requestPath = requestPath;
        }
        public string FilePath { get { return _filePath; } }
        public string RequestPath { get { return _requestPath; } }
        public DateTime Expires { get { return _expires; } }
        public DateTime ExpiresForDeleteFile { get { return _expiresForDeleteFile; } }
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }
    }
}
