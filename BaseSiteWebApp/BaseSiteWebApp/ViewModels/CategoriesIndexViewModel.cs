using BaseSiteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public IEnumerable<Categories> Categories { get; set; }
    }
}
