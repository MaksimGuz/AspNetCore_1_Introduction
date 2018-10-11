using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ViewModels
{
    public class CategoriesEditImageViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public IFormFile PictureFile { get; set; }
    }
}
