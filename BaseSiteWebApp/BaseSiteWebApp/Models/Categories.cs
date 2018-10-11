using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseSiteWebApp.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
