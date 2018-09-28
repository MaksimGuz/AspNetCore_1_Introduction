using BaseSiteWebApp.Models;
using BaseSiteWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Interfaces
{
    public interface ICategoriesService
    {
        Task<CategoriesIndexViewModel> GetCategoriesAsync();
        Task<Categories> GetByIdAsync(int id);
    }
}
