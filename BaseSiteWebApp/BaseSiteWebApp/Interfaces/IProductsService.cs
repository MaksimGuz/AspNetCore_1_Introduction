using BaseSiteWebApp.Models;
using BaseSiteWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetAllAsync();
        Task<Products> GetByIdAsync(int id);
        Task Update(Products products);
        Task Create(Products products);
        Task Delete(Products products);
    }
}
