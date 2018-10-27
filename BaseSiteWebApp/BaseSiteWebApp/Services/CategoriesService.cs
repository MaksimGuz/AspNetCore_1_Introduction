using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.ViewModels;
using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.IO;
using Microsoft.AspNetCore.Http.Internal;

namespace BaseSiteWebApp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<CategoriesIndexViewModel> GetAllAsync()
        {
            return new CategoriesIndexViewModel { Categories = await _categoriesRepository.GetAllAsync() };
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
            return await _categoriesRepository.GetByIdAsync(id);
        }

        public async Task<CategoriesEditImageViewModel> GetCategoriesEditImageViewModelByIdAsync(int id)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);
            var model = new CategoriesEditImageViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                PictureFile = new FormFile(new MemoryStream(category.Picture), 0, category.Picture.Length, category.CategoryName, "image.bmp")
            };        
            return model;
        }

        public async Task<SelectList> GetCategoriesSelectListAsync(int? id = null)
        {
            return new SelectList(await _categoriesRepository.GetAllAsync(), "CategoryId", "CategoryName", id);
        }
        public async Task Update(Categories categories)
        {
            await _categoriesRepository.Update(categories);
        }

        public async Task UpdateFromCategoriesEditImageViewModel(CategoriesEditImageViewModel model)
        {
            if (model == null || model.CategoryId == 0)
                return;
            var categories = await GetByIdAsync(model.CategoryId);
            if (!string.IsNullOrEmpty(model.CategoryName))
                categories.CategoryName = model.CategoryName;
            if (model != null && model.PictureFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.PictureFile.CopyToAsync(memoryStream);
                    categories.Picture = memoryStream.ToArray();
                }
            }
            await Update(categories);
        }
    }
}
