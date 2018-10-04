using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.ViewModels;
using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<SelectList> GetCategoriesSelectListAsync(int? id = null)
        {
            return new SelectList(await _categoriesRepository.GetAllAsync(), "CategoryId", "CategoryName", id);
        }
    }
}
