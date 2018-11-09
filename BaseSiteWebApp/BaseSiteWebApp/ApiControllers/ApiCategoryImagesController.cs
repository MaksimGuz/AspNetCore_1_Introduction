using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseSiteWebApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSiteWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCategoryImagesController : Controller
    {
        private ICategoriesService _categoriesService;

        public ApiCategoryImagesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categories = await _categoriesService.GetByIdAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            if (categories.Picture != null)
                return File(categories.Picture, "image/bmp");
            else
                return File(new byte[0], "image/bmp");
        }
    }
}