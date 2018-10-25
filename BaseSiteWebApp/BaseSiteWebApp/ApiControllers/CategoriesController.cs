using BaseSiteWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoriesService.GetAllAsync());
        }
    }
}
