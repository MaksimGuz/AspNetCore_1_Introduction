using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace BaseSiteWebApp.Controllers
{
    [Authorize]
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

        // GET: Categories/Image/5
        public async Task<IActionResult> Image(int? id)
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

            if (categories.Picture != null)
                return File(categories.Picture, "image/bmp");
            else
                return File(new byte[0], "image/bmp");
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> EditImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _categoriesService.GetCategoriesEditImageViewModelByIdAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(int id, [Bind("CategoryId,CategoryName,PictureFile")] CategoriesEditImageViewModel model)
        {
            if (id != model.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriesService.UpdateFromCategoriesEditImageViewModel(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await CategoriesExists(model.CategoryId) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        private async Task<bool> CategoriesExists(int id)
        {
            return await _categoriesService.GetByIdAsync(id) == null;
        }
    }
}
