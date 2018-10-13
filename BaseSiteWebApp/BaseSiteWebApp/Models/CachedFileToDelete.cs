using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class CachedFileToDelete
    {
        public string Path { get; set; }
        public DateTime Expires { get; set; }
        public bool IsFileDeleted { get; set; }
    }
}
