﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class ImageCachingOptions
    {
        public string CacheDirectoryPath { get; set; }
        public int MaxImages { get; set; }
        public int ExpirationInMilliseconds { get; set; }
    }
}
