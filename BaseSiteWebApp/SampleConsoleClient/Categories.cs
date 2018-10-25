using System;
using System.Collections.Generic;
using System.Text;

namespace SampleConsoleClient
{
    public class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }

    public class CategoriesIndexViewModel
    {
        public IEnumerable<Categories> Categories { get; set; }
    }
}
