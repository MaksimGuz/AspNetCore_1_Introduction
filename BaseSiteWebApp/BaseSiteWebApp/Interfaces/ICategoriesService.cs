using BaseSiteWebApp.Models;
using BaseSiteWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Interfaces
{
    public interface ICategoriesService
    {
        Task<CategoriesIndexViewModel> GetAllAsync();
        Task<Categories> GetByIdAsync(int id);
        Task<CategoriesEditImageViewModel> GetCategoriesEditImageViewModelByIdAsync(int id);
        Task<SelectList> GetCategoriesSelectListAsync(int? id = null);
        Task Update(Categories categories);
        Task UpdateFromCategoriesEditImageViewModel(CategoriesEditImageViewModel model);
    }
}
