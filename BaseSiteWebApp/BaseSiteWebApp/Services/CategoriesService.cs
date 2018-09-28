using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.ViewModels;
using BaseSiteWebApp.Models;

namespace BaseSiteWebApp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<CategoriesIndexViewModel> GetCategoriesAsync()
        {
            return new CategoriesIndexViewModel { Categories = await _categoriesRepository.GetAllAsync() };
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
            return await _categoriesRepository.GetByIdAsync(id);
        }
    }
}
