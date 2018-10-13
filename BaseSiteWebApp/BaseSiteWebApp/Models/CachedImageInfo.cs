using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class CachedImageInfo
    {
        public string FilePath { get; set; }
        public DateTime Expires { get; set; }
        public string ContentType { get; set; }
    }
}
