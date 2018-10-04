using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.Interfaces;

namespace BaseSiteWebApp.Controllers
{
    public class CategoriesController : Controller
    {        
        private ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService )
        {
            _categoriesService = categoriesService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoriesService.GetAllAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var categories = await _categoriesService.GetByIdAsync(id.Value);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }
    }
}
