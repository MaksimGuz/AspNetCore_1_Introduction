using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseSiteWebApp.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Category")]              
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        [Range(0, 999.99)]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
