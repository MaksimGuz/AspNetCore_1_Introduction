using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class ImageCachingOptions
    {
        public string CacheDirectoryPath { get; set; }
        public int MaxImages { get; set; }
        public int CacheExpirationInSeconds { get; set; }
        public int CacheCleanupDelayInSeconds { get; set; }
        public int MaxStripesForLockAsync{ get; set; }
    }
}
